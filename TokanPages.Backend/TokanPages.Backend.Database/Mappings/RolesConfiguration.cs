using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    [ExcludeFromCodeCoverage]
    public class RolesConfiguration : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> ABuilder)
            => ABuilder.Property(ARoles => ARoles.Id).ValueGeneratedOnAdd();
    }
}