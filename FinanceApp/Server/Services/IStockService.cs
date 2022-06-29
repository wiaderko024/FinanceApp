using FinanceApp.Server.DTO;
using FinanceApp.Server.Responses;

namespace FinanceApp.Server.Services;

public interface IStockService
{
    Task<Response<SearchStocksListDTO>> SearchStocksInPolygonApiAsync(string? search);
    Task<Response<StockDTO>> GetStockAsync(string ticker);
}