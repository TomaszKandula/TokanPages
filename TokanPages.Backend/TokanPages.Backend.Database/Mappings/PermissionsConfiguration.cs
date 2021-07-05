using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    [ExcludeFromCodeCoverage]
    public class PermissionsConfiguration : IEntityTypeConfiguration<Permissions>
    {
        public void Configure(EntityTypeBuilder<Permissions> ABuilder)
            => ABuilder.Property(APermissions => APermissions.Id).ValueGeneratedOnAdd();
    }
}