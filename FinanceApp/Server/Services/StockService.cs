using FinanceApp.Server.Data;
using FinanceApp.Server.DTO;
using FinanceApp.Server.Helpers;
using FinanceApp.Server.Models;
using FinanceApp.Server.Responses;
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

        var result = await _client.SearchStock(search);

        if (result == null)
        {
            response.StatusCode = StatusCodes.Status500InternalServerError;
            response.Message = "Problem with loading tickers short info from polygon api.";
            return response;
        }

        response.StatusCode = StatusCodes.Status200OK;
        response.Message = "OK";
        response.Result = result;

        return response;
    }
    
    // sprawdzamy czy jest w db jesli tak to zwracamy
    // jesli nie to robimy request, dodajemy do db i zwracamy

    public async Task<Response<StockDTO>> GetStockAsync(string ticker)
    {
        var response = new Response<StockDTO>();

        var stock = await _context.Stocks.SingleOrDefaultAsync(e => e.Ticker == ticker);

        if (stock != null)
        {
            response.StatusCode = StatusCodes.Status200OK;
            response.Result = CreateStockDto(stock);
            return response;
        }

        try
        {
            var newStock = await _client.GetStockFromPolygon(ticker);
            
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
                IconUrl = newStock.Results.Branding.IconUrl
            };

            await _context.AddAsync(stock);
            await _context.SaveChangesAsync();

            response.StatusCode = StatusCodes.Status200OK;
            response.Result = CreateStockDto(stock);
        
            return response;
        }
        catch (Exception)
        {
            response.StatusCode = StatusCodes.Status404NotFound;
            response.Message = "Stock not found";
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
}