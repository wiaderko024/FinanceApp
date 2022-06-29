using FinanceApp.Server.DTO;

namespace FinanceApp.Server.Helpers;

public class PolygonApiClient
{
    private static readonly string ApiKey = "DrBhGV2FAnbtbcWBQ4gaUkSaEw70PCBn";

    private HttpClient _client = new();

    public async Task<StocksListDTO> SearchStock(string? search)
    {
        var halo = new StocksListDTO();

        var request = $"https://api.polygon.io/v3/reference/tickers?search={search}&active=true&sort=ticker&order=asc&limit=1000&apiKey={ApiKey}";

        var response = await _client.GetFromJsonAsync<StocksListDTO>(request);

        var test = response.Results;
        foreach (var t in test)
        {
            Console.WriteLine(t.Name);
        }

        // Console.WriteLine("DEBUG CONUT: " + response.Stocks.Count);

        return halo;
    }
}