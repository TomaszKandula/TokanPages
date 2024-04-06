using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.User;

namespace TokanPages.Persistence.Database.Mappings.User;

[ExcludeFromCodeCoverage]
public class PermissionsConfiguration : IEntityTypeConfiguration<Permissions>
{
    public void Configure(EntityTypeBuilder<Permissions> builder)
        => builder.Property(permissions => permissions.Id).ValueGeneratedOnAdd();
}