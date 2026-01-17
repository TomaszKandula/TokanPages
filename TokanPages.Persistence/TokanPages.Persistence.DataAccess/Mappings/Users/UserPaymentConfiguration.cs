using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.DataAccess.Mappings.Users;

[ExcludeFromCodeCoverage]
public class UserPaymentConfiguration : IEntityTypeConfiguration<UserPayment>
{
    public void Configure(EntityTypeBuilder<UserPayment> builder)
    {
        builder.Property(payment => payment.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(payment => payment.User)
            .WithMany(user => user.UserPayments)
            .HasForeignKey(payment => payment.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}