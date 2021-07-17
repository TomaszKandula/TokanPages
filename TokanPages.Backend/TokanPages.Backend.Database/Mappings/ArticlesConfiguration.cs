namespace TokanPages.Backend.Database.Mappings
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public class ArticlesConfiguration : IEntityTypeConfiguration<Articles>
    {
        public void Configure(EntityTypeBuilder<Articles> ABuilder)
        {
            ABuilder.Property(AArticles => AArticles.Id).ValueGeneratedOnAdd();
            
            ABuilder
                .HasOne(AArticles => AArticles.User)
                .WithMany(AUsers => AUsers.Articles)
                .HasForeignKey(AArticles => AArticles.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Articles_Users");
        }
    }
}