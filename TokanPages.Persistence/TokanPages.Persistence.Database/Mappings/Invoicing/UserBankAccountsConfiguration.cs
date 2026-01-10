using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.User;

namespace TokanPages.Persistence.Database.Mappings.Invoicing;

[ExcludeFromCodeCoverage]
public class UserBankAccountsConfiguration : IEntityTypeConfiguration<UserBankAccount>
{
    public void Configure(EntityTypeBuilder<UserBankAccount> builder)
    { 
        builder.Property(accounts => accounts.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(invoices => invoices.User)
            .WithMany(users => users.UserBankAccounts)
            .HasForeignKey(invoices => invoices.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserBankAccounts_Users");
    }
}