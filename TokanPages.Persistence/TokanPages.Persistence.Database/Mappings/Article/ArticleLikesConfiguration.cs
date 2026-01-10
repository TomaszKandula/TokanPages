using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Articles;

namespace TokanPages.Persistence.Database.Mappings.Article;

[ExcludeFromCodeCoverage]
public class ArticleLikesConfiguration : IEntityTypeConfiguration<ArticleLike>
{
    public void Configure(EntityTypeBuilder<ArticleLike> builder)
    {
        builder.Property(articleLikes => articleLikes.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(articleLikes => articleLikes.Article)
            .WithMany(articles => articles.ArticleLikes)
            .HasForeignKey(articleLikes => articleLikes.ArticleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ArticleLikes_Articles");

        builder
            .HasOne(articleLikes => articleLikes.User)
            .WithMany(users => users.ArticleLikes)
            .HasForeignKey(articleLikes => articleLikes.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ArticleLikes_Users");
    }
}