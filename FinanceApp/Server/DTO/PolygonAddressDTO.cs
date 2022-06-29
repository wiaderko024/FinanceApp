using System.Text.Json.Serialization;

namespace FinanceApp.Server.DTO;

public class PolygonAddressDTO
{
    public string Address1 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    [JsonPropertyName("postal_code")]
    public string PostalCode { get; set; }
}