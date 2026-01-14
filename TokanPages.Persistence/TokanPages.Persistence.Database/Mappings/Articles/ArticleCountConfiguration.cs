using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Articles;

namespace TokanPages.Persistence.Database.Mappings.Articles;

[ExcludeFromCodeCoverage]
public class ArticleCountConfiguration : IEntityTypeConfiguration<ArticleCount>
{
    public void Configure(EntityTypeBuilder<ArticleCount> builder)
    {
        builder.Property(count => count.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(count => count.Article)
            .WithMany(article => article.ArticleCounts)
            .HasForeignKey(count => count.ArticleId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder
            .HasOne(count => count.User)
            .WithMany(user => user.ArticleCounts)
            .HasForeignKey(count => count.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}