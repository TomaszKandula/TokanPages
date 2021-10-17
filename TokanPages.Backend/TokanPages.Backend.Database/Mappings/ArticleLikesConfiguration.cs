namespace TokanPages.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class ArticleLikesConfiguration : IEntityTypeConfiguration<ArticleLikes>
    {
        public void Configure(EntityTypeBuilder<ArticleLikes> typeBuilder)
        {
            typeBuilder.Property(articleLikes => articleLikes.Id).ValueGeneratedOnAdd();
            
            typeBuilder
                .HasOne(articleLikes => articleLikes.Article)
                .WithMany(articles => articles.ArticleLikes)
                .HasForeignKey(articleLikes => articleLikes.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleLikes_Articles");

            typeBuilder
                .HasOne(articleLikes => articleLikes.User)
                .WithMany(users => users.ArticleLikes)
                .HasForeignKey(articleLikes => articleLikes.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleLikes_Users");
        }
    }
}