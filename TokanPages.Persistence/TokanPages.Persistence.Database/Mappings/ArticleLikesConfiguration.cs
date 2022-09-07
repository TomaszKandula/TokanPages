using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class ArticleLikesConfiguration : IEntityTypeConfiguration<ArticleLikes>
{
    public void Configure(EntityTypeBuilder<ArticleLikes> builder)
    {
        builder.Property(articleLikes => articleLikes.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(articleLikes => articleLikes.ArticleNavigation)
            .WithMany(articles => articles.ArticleLikesNavigation)
            .HasForeignKey(articleLikes => articleLikes.ArticleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ArticleLikes_Articles");

        builder
            .HasOne(articleLikes => articleLikes.UserNavigation)
            .WithMany(users => users.ArticleLikesNavigation)
            .HasForeignKey(articleLikes => articleLikes.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ArticleLikes_Users");
    }
}