using FinanceApp.Server.DTO;

namespace FinanceApp.Server.Helpers;

public class PolygonApiClient
{
    private const string ApiKey = "DrBhGV2FAnbtbcWBQ4gaUkSaEw70PCBn";

    private readonly HttpClient _client = new();

    public async Task<SearchStocksListDTO?> SearchStock(string? search)
    {
        var request = $"https://api.polygon.io/v3/reference/tickers?search={search}&active=true&sort=ticker&order=asc&limit=1000&apiKey={ApiKey}";
        return await _client.GetFromJsonAsync<SearchStocksListDTO>(request);
    }
}