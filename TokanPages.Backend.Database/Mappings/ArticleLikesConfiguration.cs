﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Mappings
{
    public class ArticleLikesConfiguration : IEntityTypeConfiguration<ArticleLikes>
    {
        public void Configure(EntityTypeBuilder<ArticleLikes> AModelBuilder)
        {
            AModelBuilder.Property(AArticleLikes => AArticleLikes.Id).ValueGeneratedOnAdd();
            
            AModelBuilder
                .HasOne(AArticleLikes => AArticleLikes.Article)
                .WithMany(AArticles => AArticles.ArticleLikes)
                .HasForeignKey(AArticleLikes => AArticleLikes.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleLikes_Articles");

            AModelBuilder
                .HasOne(AArticleLikes => AArticleLikes.User)
                .WithMany(AUsers => AUsers.ArticleLikes)
                .HasForeignKey(AArticleLikes => AArticleLikes.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleLikes_Users");
        }
    }
}
