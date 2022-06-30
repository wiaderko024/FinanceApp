using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceApp.Server.Models.Configurations;

public class SubscriptionEfConfiguration : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.HasKey(e => new {e.IdUser, e.IdStock}).HasName("Subscription_pk");

        builder.HasOne(e => e.User)
            .WithMany(e => e.Subscriptions)
            .HasForeignKey(e => e.IdUser)
            .HasConstraintName("Subscription_User_fk")
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.HasOne(e => e.Stock)
            .WithMany(e => e.Subscriptions)
            .HasForeignKey(e => e.IdStock)
            .HasConstraintName("Subscription_Stock_fk")
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder.ToTable("Subscription");
    }
}