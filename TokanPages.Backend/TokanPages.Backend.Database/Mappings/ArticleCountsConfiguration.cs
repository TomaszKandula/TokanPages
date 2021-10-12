namespace TokanPages.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class ArticleCountsConfiguration : IEntityTypeConfiguration<ArticleCounts>
    {
        public void Configure(EntityTypeBuilder<ArticleCounts> ABuilder)
        {
            ABuilder.Property(AArticleCounts => AArticleCounts.Id).ValueGeneratedOnAdd();
            
            ABuilder
                .HasOne(AArticleCounts => AArticleCounts.Article)
                .WithMany(AArticles => AArticles.ArticleCounts)
                .HasForeignKey(AArticleCounts => AArticleCounts.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleCounts_Articles");

            ABuilder
                .HasOne(AArticleCounts => AArticleCounts.User)
                .WithMany(AUsers => AUsers.ArticleCounts)
                .HasForeignKey(AArticleCounts => AArticleCounts.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleCounts_Users");
        }
    }
}