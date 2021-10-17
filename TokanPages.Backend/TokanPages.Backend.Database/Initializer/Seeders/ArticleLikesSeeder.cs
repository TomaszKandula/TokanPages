namespace TokanPages.Backend.Database.Initializer.Seeders
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Domain.Entities;
    using Data.ArticleLikes;

    [ExcludeFromCodeCoverage]
    public static class ArticleLikesSeeder
    {
        public static IEnumerable<ArticleLikes> SeedArticleLikes() 
        {
            return new List<ArticleLikes>
            {
                new ()
                {
                    Id = ArticleLike1.Id,
                    ArticleId = ArticleLike1.ArticleId,
                    UserId = ArticleLike1.UserId,
                    IpAddress = ArticleLike1.IpAddress,
                    LikeCount = ArticleLike1.LikeCount
                },
                new ()
                {
                    Id = ArticleLike2.Id,
                    ArticleId = ArticleLike2.ArticleId,
                    UserId = ArticleLike2.UserId,
                    IpAddress = ArticleLike2.IpAddress,
                    LikeCount = ArticleLike2.LikeCount
                },
                new ()
                {
                    Id = ArticleLike3.Id,
                    ArticleId = ArticleLike3.ArticleId,
                    UserId = ArticleLike3.UserId,
                    IpAddress = ArticleLike3.IpAddress,
                    LikeCount = ArticleLike3.LikeCount
                },
                new ()
                {
                    Id = ArticleLike4.Id,
                    ArticleId = ArticleLike4.ArticleId,
                    UserId = ArticleLike4.UserId,
                    IpAddress = ArticleLike4.IpAddress,
                    LikeCount = ArticleLike4.LikeCount
                }
            };
        }
    }
}