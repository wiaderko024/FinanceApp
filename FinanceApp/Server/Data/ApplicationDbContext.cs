using System.Reflection;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Duende.IdentityServer.EntityFramework.Options;
using FinanceApp.Server.Models;
using FinanceApp.Server.Models.Configurations;

namespace FinanceApp.Server.Data;

public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public virtual DbSet<Stock> Stocks { get; set; }

    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StockEfConfiguration).GetTypeInfo().Assembly);

        modelBuilder.Entity<Stock>().HasIndex(e => e.Ticker).IsUnique();
    }
}
