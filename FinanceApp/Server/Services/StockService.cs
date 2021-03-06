using FinanceApp.Server.Data;
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

        if (search == null) return await AddStockInfo(search, response);
        
        var stocks = _context.Stocks.Where(e => e.Ticker.ToLower() == search.ToLower())
            .Select(e => new StockShortDTO
            {
                Name = e.Name,
                Ticker = e.Ticker
            });

        if (!stocks.Any()) return await AddStockInfo(search, response);
        
        response.StatusCode = StatusCodes.Status200OK;
        response.Result = new SearchStocksListDTO
        {
            Results = stocks
        };
        
        return response;
    }

    public async Task<Response<StockDTO>> GetStockAsync(string ticker)
    {
        var response = new Response<StockDTO>();

        var stock = await _context.Stocks.SingleOrDefaultAsync(e => e.Ticker.ToLower() == ticker.ToLower());

        if (stock is {HasData: true})
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.Result = CreateStockDto(stock);
            return response;
        }

        try
        {
            var newStock = await _client.GetStockFromPolygon(ticker);
            
            stock = await _context.Stocks.SingleOrDefaultAsync(e => e.Ticker.ToLower() == newStock.Results.Ticker.ToLower());

            if (stock != null && stock.HasData == false)
            {
                stock.Locale = newStock.Results.Locale;
                stock.Active = newStock.Results.Active;
                stock.CurrencyName = newStock.Results.CurrencyName;
                stock.PrimaryExchange = newStock.Results.PrimaryExchange;
                stock.PhoneNumber = newStock.Results.PhoneNumber;
                stock.Address1 = newStock.Results.Address == null ? null : newStock.Results.Address.Address1;
                stock.City = newStock.Results.Address == null ? null : newStock.Results.Address.City;
                stock.State = newStock.Results.Address == null ? null : newStock.Results.Address.State;
                stock.PostalCode = newStock.Results.Address == null ? null : newStock.Results.Address.PostalCode;
                stock.Description = newStock.Results.Description;
                stock.HomePageUrl = newStock.Results.HomePageUrl;
                stock.TotalEmployees = newStock.Results.TotalEmployees;
                stock.ListDate = newStock.Results.ListDate;
                stock.LogoUrl = newStock.Results.Branding == null ? null : $"{newStock.Results.Branding.LogoUrl}?apiKey=DrBhGV2FAnbtbcWBQ4gaUkSaEw70PCBn";
                stock.IconUrl = newStock.Results.Branding == null ? null : $"{newStock.Results.Branding.IconUrl}?apiKey=DrBhGV2FAnbtbcWBQ4gaUkSaEw70PCBn";
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
                    LogoUrl = $"{newStock.Results.Branding.LogoUrl}?apiKey=DrBhGV2FAnbtbcWBQ4gaUkSaEw70PCBn",
                    IconUrl = $"{newStock.Results.Branding.IconUrl}?apiKey=DrBhGV2FAnbtbcWBQ4gaUkSaEw70PCBn",
                    HasData = true
                };
                await _context.AddAsync(stock);
            }
            
            await _context.SaveChangesAsync();

            var stockDto = CreateStockDto(stock);

            if (stockDto != null)
            {
                response.StatusCode = StatusCodes.Status200OK;
                response.Result = stockDto;
            }
            else
            {
                response.StatusCode = StatusCodes.Status404NotFound;
                response.Message = "Stock not found";
            }
            
            return response;
        }
        catch (Exception e)
        {
            response.StatusCode = StatusCodes.Status500InternalServerError;
            response.Message = "Polygon api doesn't response";
            return response;
        }
    }

    public async Task<Response<PolygonArticleDTO>> GetArticlesAsync(string ticker)
    {
        var response = new Response<PolygonArticleDTO>();
        try
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.Result = await _client.GetArticlesFromTicker(ticker);
            return response;
        }
        catch (Exception e)
        {
            response.StatusCode = StatusCodes.Status500InternalServerError;
            response.Message = "Polygon api doesn't response";
            return response;
        }
    }

    public async Task<Response<PolygonFinanceDTO>> GetFinancesAsync(string ticker)
    {
        var response = new Response<PolygonFinanceDTO>();
        try
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.Result = await _client.GetFinancesFromTicker(ticker);
            return response;
        }
        catch (Exception)
        {
            response.StatusCode = StatusCodes.Status500InternalServerError;
            response.Message = "Polygon api doesn't response";
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
                var stock = await _context.Stocks.SingleOrDefaultAsync(e => e.Ticker.ToLower() == newStock.Ticker.ToLower());
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