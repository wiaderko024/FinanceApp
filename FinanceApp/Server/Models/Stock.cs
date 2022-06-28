namespace FinanceApp.Server.Models;

public class Stock
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
}