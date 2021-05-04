using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    public class ArticlesConfiguration : IEntityTypeConfiguration<Articles>
    {
        public void Configure(EntityTypeBuilder<Articles> AModelBuilder)
        {
            AModelBuilder.Property(AArticles => AArticles.Id).ValueGeneratedNever();
            
            AModelBuilder
                .HasOne(AArticles => AArticles.User)
                .WithMany(AUsers => AUsers.Articles)
                .HasForeignKey(AArticles => AArticles.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Articles_Users");
        }
    }
}
