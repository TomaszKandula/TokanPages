using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.Database.Initializer.Data.ArticleLikes;

namespace TokanPages.Persistence.MigrationRunner.DataSeeder.Seeders;

[ExcludeFromCodeCoverage]
public static class ArticleLikesSeeder
{
    public static IEnumerable<ArticleLikes> SeedArticleLikes() 
    {
        return new List<ArticleLikes>
        {
            new()
            {
                Id = ArticleLike1.Id,
                ArticleId = ArticleLike1.ArticleId,
                UserId = ArticleLike1.UserId,
                IpAddress = ArticleLike1.IpAddress,
                LikeCount = ArticleLike1.LikeCount,
                CreatedAt = ArticleLike1.CreatedAt,
                CreatedBy = ArticleLike1.CreatedBy,
                ModifiedAt = ArticleLike1.ModifiedAt,
                ModifiedBy = ArticleLike1.ModifiedBy
            },
            new()
            {
                Id = ArticleLike2.Id,
                ArticleId = ArticleLike2.ArticleId,
                UserId = ArticleLike2.UserId,
                IpAddress = ArticleLike2.IpAddress,
                LikeCount = ArticleLike2.LikeCount,
                CreatedAt = ArticleLike2.CreatedAt,
                CreatedBy = ArticleLike2.CreatedBy,
                ModifiedAt = ArticleLike2.ModifiedAt,
                ModifiedBy = ArticleLike2.ModifiedBy
            },
            new()
            {
                Id = ArticleLike3.Id,
                ArticleId = ArticleLike3.ArticleId,
                UserId = ArticleLike3.UserId,
                IpAddress = ArticleLike3.IpAddress,
                LikeCount = ArticleLike3.LikeCount,
                CreatedAt = ArticleLike3.CreatedAt,
                CreatedBy = ArticleLike3.CreatedBy,
                ModifiedAt = ArticleLike3.ModifiedAt,
                ModifiedBy = ArticleLike3.ModifiedBy
            },
            new()
            {
                Id = ArticleLike4.Id,
                ArticleId = ArticleLike4.ArticleId,
                UserId = ArticleLike4.UserId,
                IpAddress = ArticleLike4.IpAddress,
                LikeCount = ArticleLike4.LikeCount,
                CreatedAt = ArticleLike4.CreatedAt,
                CreatedBy = ArticleLike4.CreatedBy,
                ModifiedAt = ArticleLike4.ModifiedAt,
                ModifiedBy = ArticleLike4.ModifiedBy
            }
        };
    }
}