using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Photography;

namespace TokanPages.Persistence.DataAccess.Mappings.Photography;

[ExcludeFromCodeCoverage]
public class PhotoGearConfiguration : IEntityTypeConfiguration<PhotoGear>
{
    public void Configure(EntityTypeBuilder<PhotoGear> builder)
    {
        builder.Property(gear => gear.Id).ValueGeneratedOnAdd();
        builder.Property(gear => gear.Aperture).HasColumnType("decimal(18,2)");
    }
}