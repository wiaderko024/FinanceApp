namespace FinanceApp.Shared.DTO;

public class StockDTO
{
    public string Name { get; set; }
    public string Ticker { get; set; }
    public string? CurrencyName { get; set; }
    public string? PhoneNumber { get; set; }
    public PolygonAddressDTO? Address { get; set; }
    public string? Description { get; set; }
    public string? HomepageUrl { get; set; }
    public DateTime? ListDate { get; set; }
    public int? TotalEmployees { get; set; }
    public PolygonBrandingDTO? Branding { get; set; }
}