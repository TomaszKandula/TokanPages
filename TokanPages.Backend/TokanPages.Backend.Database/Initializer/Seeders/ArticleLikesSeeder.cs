using System.Collections.Generic;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Database.Initializer.Data;

namespace TokanPages.Backend.Database.Initializer.Seeders
{
    public static class ArticleLikesSeeder
    {
        public static IEnumerable<ArticleLikes> SeedArticleLikes() 
        {
            return new List<ArticleLikes>
            {
                new ()
                {
                    Id = ArticleLikes1.FId,
                    ArticleId = ArticleLikes1.FArticleId,
                    UserId = ArticleLikes1.FUserId,
                    IpAddress = ArticleLikes1.IP_ADDRESS,
                    LikeCount = ArticleLikes1.LIKE_COUNT
                },
                new ()
                {
                    Id = ArticleLikes2.FId,
                    ArticleId = ArticleLikes2.FArticleId,
                    UserId = ArticleLikes2.FUserId,
                    IpAddress = ArticleLikes2.IP_ADDRESS,
                    LikeCount = ArticleLikes2.LIKE_COUNT
                },
                new ()
                {
                    Id = ArticleLikes3.FId,
                    ArticleId = ArticleLikes3.FArticleId,
                    UserId = ArticleLikes3.FUserId,
                    IpAddress = ArticleLikes3.IP_ADDRESS,
                    LikeCount = ArticleLikes3.LIKE_COUNT
                },
                new ()
                {
                    Id = ArticleLikes4.FId,
                    ArticleId = ArticleLikes4.FArticleId,
                    UserId = ArticleLikes4.FUserId,
                    IpAddress = ArticleLikes4.IP_ADDRESS,
                    LikeCount = ArticleLikes4.LIKE_COUNT
                }
            };
        }
    }
}
