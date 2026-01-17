using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities.Articles;

namespace TokanPages.Persistence.DataAccess.Mappings.Articles;

[ExcludeFromCodeCoverage]
public class ArticleCategoryConfiguration : IEntityTypeConfiguration<ArticleCategory>
{
    public void Configure(EntityTypeBuilder<ArticleCategory> builder)
        => builder.Property(articleCategory => articleCategory.Id).ValueGeneratedOnAdd();
}