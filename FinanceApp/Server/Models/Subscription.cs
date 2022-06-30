namespace FinanceApp.Server.Models;

public class Subscription
{
    public string IdUser { get; set; }
    public int IdStock { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual Stock Stock { get; set; }
}