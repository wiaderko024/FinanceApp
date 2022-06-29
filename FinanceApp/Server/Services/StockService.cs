using FinanceApp.Server.DTO;
using FinanceApp.Server.Helpers;
using FinanceApp.Server.Responses;

namespace FinanceApp.Server.Services;

public class StockService : IStockService
{
    private readonly PolygonApiClient _client = new();

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

    public async Task<Response<StockDTO>> GetStockAsync(string ticker)
    {
        var response = new Response<StockDTO>();
        
        // todo
        
        return response;
    }
}