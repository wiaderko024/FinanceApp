using Microsoft.AspNetCore.Identity;

namespace FinanceApp.Server.Models;

public class ApplicationUser : IdentityUser
{
    public virtual ICollection<Subscription> Subscriptions { get; set; }

    public ApplicationUser()
    {
        Subscriptions = new HashSet<Subscription>();
    }
}
