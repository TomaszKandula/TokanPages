using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Articles;

namespace TokanPages.Persistence.DataAccess.Mappings.Articles;

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
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}