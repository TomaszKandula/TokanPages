using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings.User;

[ExcludeFromCodeCoverage]
public class UserPaymentsHistoryConfiguration : IEntityTypeConfiguration<UserPaymentHistory>
{
    public void Configure(EntityTypeBuilder<UserPaymentHistory> builder)
    {
        builder.Property(payments => payments.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(payments => payments.Users)
            .WithMany(users => users.UserPaymentHistory)
            .HasForeignKey(payments => payments.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserPaymentsHistory_Users");
    }
}