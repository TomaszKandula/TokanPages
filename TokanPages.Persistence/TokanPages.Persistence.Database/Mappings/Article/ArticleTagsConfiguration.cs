using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Article;

namespace TokanPages.Persistence.Database.Mappings.Article;

[ExcludeFromCodeCoverage]
public class ArticleTagsConfiguration : IEntityTypeConfiguration<ArticleTag>
{
    public void Configure(EntityTypeBuilder<ArticleTag> builder)
    {
        builder.Property(articleCounts => articleCounts.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(articleTags => articleTags.Article)
            .WithMany(articles => articles.ArticleTags)
            .HasForeignKey(articleTags => articleTags.ArticleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ArticleTags_Articles");
    }
}