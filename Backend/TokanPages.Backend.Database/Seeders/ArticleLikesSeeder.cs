using System.Collections.Generic;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Database.Dummies;

namespace TokanPages.Backend.Database.Seeders
{
    public class ArticleLikesSeeder
    {
        public static List<ArticleLikes> SeedArticleLikes() 
        {
            return new List<ArticleLikes>
            {
                new ArticleLikes
                {
                    Id = ArticleLikes1.Id,
                    ArticleId = ArticleLikes1.ArticleId,
                    UserId = ArticleLikes1.UserId,
                    IpAddress = ArticleLikes1.IpAddress,
                    LikeCount = ArticleLikes1.LikeCount
                },
                new ArticleLikes
                {
                    Id = ArticleLikes2.Id,
                    ArticleId = ArticleLikes2.ArticleId,
                    UserId = ArticleLikes2.UserId,
                    IpAddress = ArticleLikes2.IpAddress,
                    LikeCount = ArticleLikes2.LikeCount
                },
                new ArticleLikes
                {
                    Id = ArticleLikes3.Id,
                    ArticleId = ArticleLikes3.ArticleId,
                    UserId = ArticleLikes3.UserId,
                    IpAddress = ArticleLikes3.IpAddress,
                    LikeCount = ArticleLikes3.LikeCount
                },
                new ArticleLikes
                {
                    Id = ArticleLikes4.Id,
                    ArticleId = ArticleLikes4.ArticleId,
                    UserId = ArticleLikes4.UserId,
                    IpAddress = ArticleLikes4.IpAddress,
                    LikeCount = ArticleLikes4.LikeCount
                }
            };
        }
    }
}
