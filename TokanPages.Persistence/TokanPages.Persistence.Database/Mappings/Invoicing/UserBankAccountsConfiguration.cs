using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Persistence.Database.Mappings.Invoicing;

[ExcludeFromCodeCoverage]
public class UserBankAccountsConfiguration : IEntityTypeConfiguration<UserBankAccounts>
{
    public void Configure(EntityTypeBuilder<UserBankAccounts> builder)
        => builder.Property(accounts => accounts.Id).ValueGeneratedOnAdd();
}