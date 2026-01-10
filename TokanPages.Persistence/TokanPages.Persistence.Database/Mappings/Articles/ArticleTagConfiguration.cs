using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Articles;

namespace TokanPages.Persistence.Database.Mappings.Articles;

[ExcludeFromCodeCoverage]
public class ArticleTagConfiguration : IEntityTypeConfiguration<ArticleTag>
{
    public void Configure(EntityTypeBuilder<ArticleTag> builder)
    {
        builder.Property(tag => tag.Id).ValueGeneratedOnAdd();

        builder
            .HasOne(tag => tag.Article)
            .WithMany(article => article.ArticleTags)
            .HasForeignKey(tag => tag.ArticleId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_ArticleTags_Articles");
    }
}