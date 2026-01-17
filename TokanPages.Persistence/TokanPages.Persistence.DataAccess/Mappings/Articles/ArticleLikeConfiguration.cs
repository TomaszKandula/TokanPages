using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Articles;

namespace TokanPages.Persistence.DataAccess.Mappings.Articles;

[ExcludeFromCodeCoverage]
public class ArticleLikeConfiguration : IEntityTypeConfiguration<ArticleLike>
{
    public void Configure(EntityTypeBuilder<ArticleLike> builder)
    {
        builder.Property(like => like.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(like => like.Article)
            .WithMany(article => article.ArticleLikes)
            .HasForeignKey(like => like.ArticleId)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder
            .HasOne(like => like.User)
            .WithMany(user => user.ArticleLikes)
            .HasForeignKey(like => like.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}