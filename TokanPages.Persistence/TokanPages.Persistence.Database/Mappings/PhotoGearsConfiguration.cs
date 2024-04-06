using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Photography;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class PhotoGearsConfiguration : IEntityTypeConfiguration<PhotoGears>
{
    public void Configure(EntityTypeBuilder<PhotoGears> builder)
    {
        builder.Property(photoGears => photoGears.Id).ValueGeneratedOnAdd();
        builder.Property(photoGears => photoGears.Aperture).HasColumnType("decimal(18,2)");
    }
}