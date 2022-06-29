using System.Text.Json.Serialization;

namespace FinanceApp.Server.DTO;

public class PolygonBrandingDTO
{
    [JsonPropertyName("logo_url")]
    public string LogoUrl { get; set; }
    [JsonPropertyName("icon_url")]
    public string IconUrl { get; set; }
}