namespace TokanPages.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class ArticleLikesConfiguration : IEntityTypeConfiguration<ArticleLikes>
    {
        public void Configure(EntityTypeBuilder<ArticleLikes> ABuilder)
        {
            ABuilder.Property(AArticleLikes => AArticleLikes.Id).ValueGeneratedOnAdd();
            
            ABuilder
                .HasOne(AArticleLikes => AArticleLikes.Article)
                .WithMany(AArticles => AArticles.ArticleLikes)
                .HasForeignKey(AArticleLikes => AArticleLikes.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleLikes_Articles");

            ABuilder
                .HasOne(AArticleLikes => AArticleLikes.User)
                .WithMany(AUsers => AUsers.ArticleLikes)
                .HasForeignKey(AArticleLikes => AArticleLikes.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleLikes_Users");
        }
    }
}