using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Invoicing;

namespace TokanPages.Persistence.Database.Mappings.Invoicing;

[ExcludeFromCodeCoverage]
public class UserCompaniesConfiguration : IEntityTypeConfiguration<UserCompanies>
{
    public void Configure(EntityTypeBuilder<UserCompanies> builder)
    {
        builder.Property(userCompanies => userCompanies.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(invoices => invoices.Users)
            .WithMany(users => users.UserCompanies)
            .HasForeignKey(invoices => invoices.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_UserCompanies_Users");
    }
}