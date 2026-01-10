using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Photography;

namespace TokanPages.Persistence.Database.Mappings.Photography;

[ExcludeFromCodeCoverage]
public class PhotoCategorieConfiguration : IEntityTypeConfiguration<PhotoCategory>
{
    public void Configure(EntityTypeBuilder<PhotoCategory> builder)
        => builder.Property(category => category.Id).ValueGeneratedOnAdd();
}