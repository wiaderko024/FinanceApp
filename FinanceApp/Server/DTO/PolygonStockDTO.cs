using System.Text.Json.Serialization;

namespace FinanceApp.Server.DTO;

public class PolygonStockDTO
{
    public string Ticker { get; set; }
    public string Name { get; set; }
    public string? Locale { get; set; }
    public bool? Active { get; set; }
    [JsonPropertyName("currency_name")]
    public string? CurrencyName { get; set; }
    [JsonPropertyName("primary_exchange")]
    public string? PrimaryExchange { get; set; }
    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; }
    public PolygonAddressDTO Address { get; set; }
    public string? Description { get; set; }
    [JsonPropertyName("homepage_url")]
    public string? HomePageUrl { get; set; }
    [JsonPropertyName("total_employees")]
    public int? TotalEmployees { get; set; }
    [JsonPropertyName("list_date")]
    public DateTime? ListDate { get; set; }
    public PolygonBrandingDTO? Branding { get; set; }
}