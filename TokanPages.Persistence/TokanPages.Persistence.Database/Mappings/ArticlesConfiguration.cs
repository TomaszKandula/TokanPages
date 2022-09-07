using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Mappings;

[ExcludeFromCodeCoverage]
public class ArticlesConfiguration : IEntityTypeConfiguration<Articles>
{
    public void Configure(EntityTypeBuilder<Articles> builder)
    {
        builder.Property(articles => articles.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(articles => articles.UserNavigation)
            .WithMany(users => users.ArticlesNavigation)
            .HasForeignKey(articles => articles.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Articles_Users");
    }
}