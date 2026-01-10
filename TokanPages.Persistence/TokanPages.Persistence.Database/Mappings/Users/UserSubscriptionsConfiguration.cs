using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.Database.Mappings.Users;

[ExcludeFromCodeCoverage]
public class UserSubscriptionsConfiguration : IEntityTypeConfiguration<UserSubscription>
{
    public void Configure(EntityTypeBuilder<UserSubscription> builder)
    {
        builder.Property(subscriptions => subscriptions.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(subscriptions => subscriptions.User)
            .WithMany(users => users.UserSubscriptions)
            .HasForeignKey(subscriptions => subscriptions.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserSubscriptions_Users");
    }
}