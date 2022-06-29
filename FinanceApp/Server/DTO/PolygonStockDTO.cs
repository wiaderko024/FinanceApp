namespace FinanceApp.Server.DTO;

public class PolygonStockDTO
{
    public int IdStock { get; set; }
    public string Ticker { get; set; }
    public string Name { get; set; }
    public string Market { get; set; }
    public string Locale { get; set; }
    public string Type { get; set; }
    public bool Active { get; set; }
    public string CurrencyName { get; set; }
    public string CompositeFigi { get; set; }
    public string ShareClassFigi { get; set; }

    public string PrimaryExchange { get; set; }
    public string Cik { get; set; }
    public double MarketCap { get; set; }
    public string PhoneNumber { get; set; }
    
    public PolygonAddressDTO Address { get; set; }

    public string Description { get; set; }
    public string SicCode { get; set; }
    public string SicDescription { get; set; }
    public string TickerRoot { get; set; }
    public string HomePageUrl { get; set; }
    public int TotalEmployees { get; set; }
    public DateTime ListDate { get; set; }
    
    public PolygonBrandingDTO Branding { get; set; }
    
    public int ShareClassSharesOutstanding { get; set; }
    public int WeightedSharesOutstanding { get; set; }
}