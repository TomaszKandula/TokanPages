using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    public class ArticleLikesConfiguration : IEntityTypeConfiguration<ArticleLikes>
    {
        public void Configure(EntityTypeBuilder<ArticleLikes> AModelBuilder)
        {
            AModelBuilder.Property(e => e.Id).ValueGeneratedNever();
            
            AModelBuilder
                .HasOne(e => e.Article)
                .WithMany(e => e.ArticleLikes)
                .HasForeignKey(e => e.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleLikes_Articles");

            AModelBuilder
                .HasOne(e => e.User)
                .WithMany(e => e.ArticleLikes)
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleLikes_Users");
        }
    }
}
