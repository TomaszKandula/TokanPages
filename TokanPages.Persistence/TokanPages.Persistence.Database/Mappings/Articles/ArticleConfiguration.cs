using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings.Articles;

[ExcludeFromCodeCoverage]
public class ArticleConfiguration : IEntityTypeConfiguration<Backend.Domain.Entities.Articles.Article>
{
    public void Configure(EntityTypeBuilder<Backend.Domain.Entities.Articles.Article> builder)
    {
        builder.Property(article => article.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(article => article.User)
            .WithMany(user => user.Articles)
            .HasForeignKey(article => article.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Articles_Users");

        builder
            .HasOne(article => article.ArticleCategory)
            .WithMany(category => category.Articles)
            .HasForeignKey(article => article.CategoryId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Articles_ArticleCategory");
    }
}