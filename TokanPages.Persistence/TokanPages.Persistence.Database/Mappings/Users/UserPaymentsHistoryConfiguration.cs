using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.Database.Mappings.Users;

[ExcludeFromCodeCoverage]
public class UserPaymentsHistoryConfiguration : IEntityTypeConfiguration<UserPaymentHistory>
{
    public void Configure(EntityTypeBuilder<UserPaymentHistory> builder)
    {
        builder.Property(payments => payments.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(payments => payments.User)
            .WithMany(users => users.UserPaymentsHistory)
            .HasForeignKey(payments => payments.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserPaymentsHistory_Users");
    }
}