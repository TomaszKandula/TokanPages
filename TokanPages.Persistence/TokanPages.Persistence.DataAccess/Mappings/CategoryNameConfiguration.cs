using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Articles;

namespace TokanPages.Persistence.DataAccess.Mappings;

[ExcludeFromCodeCoverage]
public class CategoryNameConfiguration : IEntityTypeConfiguration<ArticleCategoryName>
{
    public void Configure(EntityTypeBuilder<ArticleCategoryName> builder)
    {
        builder.Property(categoryName => categoryName.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(categoryName => categoryName.Language)
            .WithMany(language => language.ArticleCategoryNames)
            .HasForeignKey(categoryName => categoryName.LanguageId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder
            .HasOne(categoryName => categoryName.ArticleCategory)
            .WithMany(articleCategory => articleCategory.ArticleCategoryNames)
            .HasForeignKey(categoryName => categoryName.ArticleCategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}