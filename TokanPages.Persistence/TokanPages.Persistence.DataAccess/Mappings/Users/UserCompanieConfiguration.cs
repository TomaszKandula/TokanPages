using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Users;

namespace TokanPages.Persistence.DataAccess.Mappings.Users;

[ExcludeFromCodeCoverage]
public class UserCompanieConfiguration : IEntityTypeConfiguration<UserCompany>
{
    public void Configure(EntityTypeBuilder<UserCompany> builder)
    {
        builder.Property(company => company.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(company => company.User)
            .WithMany(user => user.UserCompanies)
            .HasForeignKey(company => company.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}