namespace TokanPages.Backend.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class ArticleCountsConfiguration : IEntityTypeConfiguration<ArticleCounts>
{
    public void Configure(EntityTypeBuilder<ArticleCounts> builder)
    {
        builder.Property(articleCounts => articleCounts.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(articleCounts => articleCounts.Article)
            .WithMany(articles => articles.ArticleCounts)
            .HasForeignKey(articleCounts => articleCounts.ArticleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ArticleCounts_Articles");

        builder
            .HasOne(articleCounts => articleCounts.User)
            .WithMany(users => users.ArticleCounts)
            .HasForeignKey(articleCounts => articleCounts.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ArticleCounts_Users");
    }
}