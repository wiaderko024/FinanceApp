namespace FinanceApp.Shared.DTO;

public class SubscriptionDTO
{
    public string Name { get; set; }
    public string Ticker { get; set; }
    public string? LogoUrl { get; set; }
    public string? City { get; set; }
    public string? Currency { get; set; }
}