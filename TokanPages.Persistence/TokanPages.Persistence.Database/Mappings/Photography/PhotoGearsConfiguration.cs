using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Photography;

namespace TokanPages.Persistence.Database.Mappings.Photography;

[ExcludeFromCodeCoverage]
public class PhotoGearsConfiguration : IEntityTypeConfiguration<PhotoGear>
{
    public void Configure(EntityTypeBuilder<PhotoGear> builder)
    {
        builder.Property(photoGears => photoGears.Id).ValueGeneratedOnAdd();
        builder.Property(photoGears => photoGears.Aperture).HasColumnType("decimal(18,2)");
    }
}