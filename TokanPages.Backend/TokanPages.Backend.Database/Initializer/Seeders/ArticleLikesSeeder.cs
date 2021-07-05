using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Database.Initializer.Data.ArticleLikes;

namespace TokanPages.Backend.Database.Initializer.Seeders
{
    [ExcludeFromCodeCoverage]
    public static class ArticleLikesSeeder
    {
        public static IEnumerable<ArticleLikes> SeedArticleLikes() 
        {
            return new List<ArticleLikes>
            {
                new ()
                {
                    Id = ArticleLike1.FId,
                    ArticleId = ArticleLike1.FArticleId,
                    UserId = ArticleLike1.FUserId,
                    IpAddress = ArticleLike1.IP_ADDRESS,
                    LikeCount = ArticleLike1.LIKE_COUNT
                },
                new ()
                {
                    Id = ArticleLike2.FId,
                    ArticleId = ArticleLike2.FArticleId,
                    UserId = ArticleLike2.FUserId,
                    IpAddress = ArticleLike2.IP_ADDRESS,
                    LikeCount = ArticleLike2.LIKE_COUNT
                },
                new ()
                {
                    Id = ArticleLike3.FId,
                    ArticleId = ArticleLike3.FArticleId,
                    UserId = ArticleLike3.FUserId,
                    IpAddress = ArticleLike3.IP_ADDRESS,
                    LikeCount = ArticleLike3.LIKE_COUNT
                },
                new ()
                {
                    Id = ArticleLike4.FId,
                    ArticleId = ArticleLike4.FArticleId,
                    UserId = ArticleLike4.FUserId,
                    IpAddress = ArticleLike4.IP_ADDRESS,
                    LikeCount = ArticleLike4.LIKE_COUNT
                }
            };
        }
    }
}
