using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class PhotoCategoriesConfiguration : IEntityTypeConfiguration<PhotoCategories>
{
    public void Configure(EntityTypeBuilder<PhotoCategories> builder)
        => builder.Property(photoCategories => photoCategories.Id).ValueGeneratedOnAdd();
}