using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Photography;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class PhotoCategoriesConfiguration : IEntityTypeConfiguration<PhotoCategories>
{
    public void Configure(EntityTypeBuilder<PhotoCategories> builder)
        => builder.Property(photoCategories => photoCategories.Id).ValueGeneratedOnAdd();
}