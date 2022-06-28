using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceApp.Server.Models.Configurations;

public class StockEfConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.HasKey(e => e.IdStock).HasName("Stock_pk");
        builder.Property(e => e.IdStock).UseIdentityColumn();

        builder.ToTable("Stock");
    }
}