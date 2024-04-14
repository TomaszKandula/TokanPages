using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings.User;

[ExcludeFromCodeCoverage]
public class UserPaymentsConfiguration : IEntityTypeConfiguration<UserPayment>
{
    public void Configure(EntityTypeBuilder<UserPayment> builder)
    {
        builder.Property(payments => payments.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(payments => payments.User)
            .WithMany(users => users.UserPaymentNavigation)
            .HasForeignKey(payments => payments.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserPayments_Users");
    }
}