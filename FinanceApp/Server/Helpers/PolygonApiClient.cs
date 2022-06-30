using FinanceApp.Shared.DTO;
using FinanceApp.Server.Models;
using FinanceApp.Shared.DTO;

namespace FinanceApp.Server.Helpers;

public class PolygonApiClient
{
    private const string ApiKey = "DrBhGV2FAnbtbcWBQ4gaUkSaEw70PCBn";

    private readonly HttpClient _client = new();

    public async Task<SearchStocksListDTO?> SearchStock(string? search)
    {
        return await _client.GetFromJsonAsync<SearchStocksListDTO>($"https://api.polygon.io/v3/reference/tickers?search={search}&active=true&sort=ticker&order=asc&limit=1000&apiKey={ApiKey}");
    }

    public async Task<PolygonTickerDetailDTO?> GetStockFromPolygon(string ticker)
    {
        return await _client.GetFromJsonAsync<PolygonTickerDetailDTO>($"https://api.polygon.io/v3/reference/tickers/{ticker.ToUpper()}?apiKey={ApiKey}");
    }

    public async Task<PolygonArticleDTO?> GetArticlesFromTicker(string ticker)
    {
        return await _client.GetFromJsonAsync<PolygonArticleDTO>($"https://api.polygon.io/v2/reference/news?ticker={ticker.ToUpper()}&apiKey={ApiKey}");
    }

    public async Task<PolygonFinanceDTO?> GetFinancesFromTicker(string ticker)
    {
        var to = DateTime.Today;
        var from = DateTime.Today.AddMonths(-3);
        return await _client.GetFromJsonAsync<PolygonFinanceDTO>($"https://api.polygon.io/v2/aggs/ticker/{ticker.ToUpper()}/range/1/day/{from.Year}-{from.Month.ToString("00")}-{from.Day.ToString("00")}/{to.Year}-{to.Month.ToString("00")}-{to.Day.ToString("00")}?adjusted=true&sort=asc&limit=120&apiKey={ApiKey}");
    }
}