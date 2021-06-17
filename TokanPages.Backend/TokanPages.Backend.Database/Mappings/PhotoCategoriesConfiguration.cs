using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    [ExcludeFromCodeCoverage]
    public class PhotoCategoriesConfiguration : IEntityTypeConfiguration<PhotoCategories>
    {
        public void Configure(EntityTypeBuilder<PhotoCategories> ABuilder)
            => ABuilder.Property(APhotoCategories => APhotoCategories.Id).ValueGeneratedOnAdd();
    }
}
