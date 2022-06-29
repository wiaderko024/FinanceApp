namespace FinanceApp.Server.DTO;

public class StockDTO
{
    public string Name { get; set; }
    public string Ticker { get; set; }
    public string CurrencyName { get; set; }
    public string PhoneNumber { get; set; }
    
    public string Address1 { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }

    public string Description { get; set; }
    public string HomepageUrl { get; set; }
    public DateTime ListDate { get; set; }
    public int TotalEmployees { get; set; }
    
    public string LogoUrl { get; set; }
    public string IconUrl { get; set; }
}