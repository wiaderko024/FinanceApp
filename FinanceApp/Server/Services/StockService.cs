using FinanceApp.Server.Data;
using FinanceApp.Server.DTO;
using FinanceApp.Server.Helpers;
using FinanceApp.Server.Models;
using FinanceApp.Server.Responses;
using FinanceApp.Shared.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Server.Services;

public class StockService : IStockService
{
    private readonly ApplicationDbContext _context;
    private readonly PolygonApiClient _client = new();

    public StockService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Response<SearchStocksListDTO>> SearchStocksInPolygonApiAsync(string? search)
    {
        var response = new Response<SearchStocksListDTO>();

        if (search != null)
        {
            var stocks = _context.Stocks.Where(e => e.Ticker.ToLower().Contains(search.ToLower()) || e.Name.ToLower().Contains(search.ToLower()))
                .Select(e => new StockShortDTO
                {
                    Name = e.Name,
                    Ticker = e.Ticker
                });
        
            if (stocks.Any())
            {
                response.StatusCode = StatusCodes.Status200OK;
                response.Result = new SearchStocksListDTO
                {
                    Results = stocks
                };
                return response;
            }
            return await AddStockInfo(search, response);
        }

        return await AddStockInfo(search, response);
    }

    public async Task<Response<StockDTO>> GetStockAsync(string ticker)
    {
        var response = new Response<StockDTO>();

        var stock = await _context.Stocks.SingleOrDefaultAsync(e => e.Ticker == ticker);

        if (stock is {HasData: true})
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.Result = CreateStockDto(stock);
            return response;
        }

        try
        {
            var newStock = await _client.GetStockFromPolygon(ticker);
            
            stock = await _context.Stocks.SingleOrDefaultAsync(e => e.Ticker == ticker);
            if (stock != null)
            {
                stock.Locale = newStock.Results.Locale;
                stock.Active = newStock.Results.Active;
                stock.CurrencyName = newStock.Results.CurrencyName;
                stock.PrimaryExchange = newStock.Results.PrimaryExchange;
                stock.PhoneNumber = newStock.Results.PhoneNumber;
                stock.Address1 = newStock.Results.Address.Address1;
                stock.City = newStock.Results.Address.City;
                stock.State = newStock.Results.Address.State;
                stock.PostalCode = newStock.Results.Address.PostalCode;
                stock.Description = newStock.Results.Description;
                stock.HomePageUrl = newStock.Results.HomePageUrl;
                stock.TotalEmployees = newStock.Results.TotalEmployees;
                stock.ListDate = newStock.Results.ListDate;
                stock.LogoUrl = newStock.Results.Branding.LogoUrl;
                stock.IconUrl = newStock.Results.Branding.IconUrl;
                stock.HasData = true;
            }
            else
            {
                stock = new Stock
                {
                    Ticker = newStock.Results.Ticker,
                    Name = newStock.Results.Name,
                    Locale = newStock.Results.Locale,
                    Active = newStock.Results.Active,
                    CurrencyName = newStock.Results.CurrencyName,
                    PrimaryExchange = newStock.Results.PrimaryExchange,
                    PhoneNumber = newStock.Results.PhoneNumber,
                    Address1 = newStock.Results.Address.Address1,
                    City = newStock.Results.Address.City,
                    State = newStock.Results.Address.State,
                    PostalCode = newStock.Results.Address.PostalCode,
                    Description = newStock.Results.Description,
                    HomePageUrl = newStock.Results.HomePageUrl,
                    TotalEmployees = newStock.Results.TotalEmployees,
                    ListDate = newStock.Results.ListDate,
                    LogoUrl = newStock.Results.Branding.LogoUrl,
                    IconUrl = newStock.Results.Branding.IconUrl,
                    HasData = true
                };
                await _context.AddAsync(stock);
            }
            
            await _context.SaveChangesAsync();

            response.StatusCode = StatusCodes.Status200OK;
            response.Result = CreateStockDto(stock);
        
            return response;
        }
        catch (Exception)
        {
            response.StatusCode = StatusCodes.Status404NotFound;
            response.Message = "Stock not found or polygon api doesn't response";
            return response;
        }
    }

    private static StockDTO CreateStockDto(Stock stock)
    {
        return new StockDTO
        {
            Name = stock.Name,
            Ticker = stock.Ticker,
            CurrencyName = stock.CurrencyName,
            PhoneNumber = stock.PhoneNumber,
            Address = new PolygonAddressDTO
            {
                Address1 = stock.Address1,
                City = stock.City,
                PostalCode = stock.PostalCode,
                State = stock.State
            },
            Description = stock.Description,
            HomepageUrl = stock.HomePageUrl,
            ListDate = stock.ListDate,
            TotalEmployees = stock.TotalEmployees,
            Branding = new PolygonBrandingDTO
            {
                LogoUrl = stock.LogoUrl,
                IconUrl = stock.IconUrl
            }
        };
    }

    private async Task<Response<SearchStocksListDTO>> AddStockInfo(string? search, Response<SearchStocksListDTO> response)
    {
        try
        {
            var result = await _client.SearchStock(search);

            foreach (var newStock in result.Results)
            {
                var stock = await _context.Stocks.SingleOrDefaultAsync(e => string.Equals(e.Ticker, search, StringComparison.CurrentCultureIgnoreCase));
                if (stock == null)
                {
                    await _context.Stocks.AddAsync(new Stock
                    {
                        Name = newStock.Name,
                        Ticker = newStock.Ticker,
                        HasData = false
                    });
                }
            }

            await _context.SaveChangesAsync();

            response.StatusCode = StatusCodes.Status200OK;
            response.Message = "OK";
            response.Result = result;

            return response;
        }
        catch (SqlException)
        {
            response.StatusCode = StatusCodes.Status400BadRequest;
            response.Message = "Cannot add another stock with this same ticker";
            return response;
        }
        catch (Exception)
        {
            response.StatusCode = StatusCodes.Status500InternalServerError;
            response.Message = "Problem with loading tickers short info from polygon api.";
            return response;
        }
    }
}