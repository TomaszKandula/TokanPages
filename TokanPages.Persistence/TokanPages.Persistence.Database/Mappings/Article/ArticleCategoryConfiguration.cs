using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Article;

namespace TokanPages.Persistence.Database.Mappings.Article;

[ExcludeFromCodeCoverage]
public class ArticleCategoryConfiguration : IEntityTypeConfiguration<ArticleCategory>
{
    public void Configure(EntityTypeBuilder<ArticleCategory> builder)
    {
        builder.Property(articleCategory => articleCategory.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(articleCategory => articleCategory.Language)
            .WithMany(articleCategory => articleCategory.ArticleCategory)
            .HasForeignKey(articleCategory => articleCategory.LanguageId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ArticleCategory_Language");
    }
}