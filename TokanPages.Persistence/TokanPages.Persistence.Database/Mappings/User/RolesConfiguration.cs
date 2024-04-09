using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.User;

namespace TokanPages.Persistence.Database.Mappings.User;

[ExcludeFromCodeCoverage]
public class RolesConfiguration : IEntityTypeConfiguration<Roles>
{
    public void Configure(EntityTypeBuilder<Roles> builder)
        => builder.Property(roles => roles.Id).ValueGeneratedOnAdd();
}