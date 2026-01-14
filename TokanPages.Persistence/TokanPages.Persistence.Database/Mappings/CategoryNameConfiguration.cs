using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class CategoryNameConfiguration : IEntityTypeConfiguration<CategoryName>
{
    public void Configure(EntityTypeBuilder<CategoryName> builder)
    {
        builder.Property(categoryName => categoryName.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(categoryName => categoryName.Language)
            .WithMany(language => language.CategoryNames)
            .HasForeignKey(categoryName => categoryName.LanguageId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder
            .HasOne(categoryName => categoryName.ArticleCategory)
            .WithMany(articleCategory => articleCategory.CategoryNames)
            .HasForeignKey(categoryName => categoryName.ArticleCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}