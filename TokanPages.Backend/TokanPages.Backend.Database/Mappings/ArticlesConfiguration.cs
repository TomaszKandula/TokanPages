namespace TokanPages.Backend.Database.Mappings;

using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public class ArticlesConfiguration : IEntityTypeConfiguration<Articles>
{
    public void Configure(EntityTypeBuilder<Articles> builder)
    {
        builder.Property(articles => articles.Id).ValueGeneratedOnAdd();
            
        builder
            .HasOne(articles => articles.User)
            .WithMany(users => users.Articles)
            .HasForeignKey(articles => articles.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Articles_Users");
    }
}