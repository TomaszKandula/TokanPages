using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Photography;

namespace TokanPages.Persistence.Database.Mappings.Photography;

[ExcludeFromCodeCoverage]
public class PhotoCategoriesConfiguration : IEntityTypeConfiguration<PhotoCategory>
{
    public void Configure(EntityTypeBuilder<PhotoCategory> builder)
        => builder.Property(photoCategories => photoCategories.Id).ValueGeneratedOnAdd();
}