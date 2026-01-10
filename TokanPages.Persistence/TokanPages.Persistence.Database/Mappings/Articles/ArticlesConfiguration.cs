using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings.Articles;

[ExcludeFromCodeCoverage]
public class ArticlesConfiguration : IEntityTypeConfiguration<Backend.Domain.Entities.Articles.Article>
{
    public void Configure(EntityTypeBuilder<Backend.Domain.Entities.Articles.Article> builder)
    {
        builder.Property(articles => articles.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(articles => articles.User)
            .WithMany(users => users.Articles)
            .HasForeignKey(articles => articles.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Articles_Users");

        builder
            .HasOne(articles => articles.ArticleCategory)
            .WithMany(articleCategory => articleCategory.Articles)
            .HasForeignKey(articleCategory => articleCategory.CategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Articles_ArticleCategory");
    }
}