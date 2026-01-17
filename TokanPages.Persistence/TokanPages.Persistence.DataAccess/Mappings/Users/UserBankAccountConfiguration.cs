using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.DataAccess.Mappings.Users;

[ExcludeFromCodeCoverage]
public class UserBankAccountConfiguration : IEntityTypeConfiguration<UserBankAccount>
{
    public void Configure(EntityTypeBuilder<UserBankAccount> builder)
    { 
        builder.Property(account => account.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(account => account.User)
            .WithMany(user => user.UserBankAccounts)
            .HasForeignKey(account => account.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}