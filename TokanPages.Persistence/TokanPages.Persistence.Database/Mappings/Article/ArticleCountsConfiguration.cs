using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Article;

namespace TokanPages.Persistence.Database.Mappings.Article;

[ExcludeFromCodeCoverage]
public class ArticleCountsConfiguration : IEntityTypeConfiguration<ArticleCounts>
{
    public void Configure(EntityTypeBuilder<ArticleCounts> builder)
    {
        builder.Property(articleCounts => articleCounts.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(articleCounts => articleCounts.Articles)
            .WithMany(articles => articles.ArticleCounts)
            .HasForeignKey(articleCounts => articleCounts.ArticleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ArticleCounts_Articles");

        builder
            .HasOne(articleCounts => articleCounts.Users)
            .WithMany(users => users.ArticleCounts)
            .HasForeignKey(articleCounts => articleCounts.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ArticleCounts_Users");
    }
}