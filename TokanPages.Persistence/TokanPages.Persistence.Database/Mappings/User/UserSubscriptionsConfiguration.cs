using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings.User;

[ExcludeFromCodeCoverage]
public class UserSubscriptionsConfiguration : IEntityTypeConfiguration<UserSubscription>
{
    public void Configure(EntityTypeBuilder<UserSubscription> builder)
    {
        builder.Property(subscriptions => subscriptions.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(subscriptions => subscriptions.User)
            .WithMany(users => users.UserSubscriptionNavigation)
            .HasForeignKey(subscriptions => subscriptions.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserSubscriptions_Users");
    }
}