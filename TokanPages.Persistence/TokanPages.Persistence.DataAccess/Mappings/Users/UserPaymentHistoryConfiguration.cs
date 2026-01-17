using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.DataAccess.Mappings.Users;

[ExcludeFromCodeCoverage]
public class UserPaymentHistoryConfiguration : IEntityTypeConfiguration<UserPaymentHistory>
{
    public void Configure(EntityTypeBuilder<UserPaymentHistory> builder)
    {
        builder.Property(history => history.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(history => history.User)
            .WithMany(user => user.UserPaymentsHistory)
            .HasForeignKey(history => history.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}