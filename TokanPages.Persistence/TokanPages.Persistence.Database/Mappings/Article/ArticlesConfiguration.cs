using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TokanPages.Persistence.Database.Mappings.Article;

[ExcludeFromCodeCoverage]
public class ArticlesConfiguration : IEntityTypeConfiguration<Backend.Domain.Entities.Article.Articles>
{
    public void Configure(EntityTypeBuilder<Backend.Domain.Entities.Article.Articles> builder)
    {
        builder.Property(articles => articles.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(articles => articles.UserNavigation)
            .WithMany(users => users.Articles)
            .HasForeignKey(articles => articles.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Articles_Users");
    }
}