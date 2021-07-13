namespace TokanPages.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class PhotoCategoriesConfiguration : IEntityTypeConfiguration<PhotoCategories>
    {
        public void Configure(EntityTypeBuilder<PhotoCategories> ABuilder)
            => ABuilder.Property(APhotoCategories => APhotoCategories.Id).ValueGeneratedOnAdd();
    }
}