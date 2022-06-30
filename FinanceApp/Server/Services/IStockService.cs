using FinanceApp.Shared.DTO;
using FinanceApp.Server.Responses;
using FinanceApp.Shared.DTO;

namespace FinanceApp.Server.Services;

public interface IStockService
{
    Task<Response<SearchStocksListDTO>> SearchStocksInPolygonApiAsync(string? search);
    Task<Response<StockDTO>> GetStockAsync(string ticker);
    Task<Response<PolygonArticleDTO>> GetArticlesAsync(string ticker);
    Task<Response<PolygonFinanceDTO>> GetFinancesAsync(string ticker);
}