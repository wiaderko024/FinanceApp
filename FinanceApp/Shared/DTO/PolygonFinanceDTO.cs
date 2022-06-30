using System.Text.Json.Serialization;

namespace FinanceApp.Shared.DTO;

public class PolygonFinanceDTO
{
    [JsonPropertyName("results")]
    public IEnumerable<FinanceDTO> Finances { get; set; }
}