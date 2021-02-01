using System.Collections.Generic;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Database.Dummies;

namespace TokanPages.Backend.Database.Seeders
{
    public class LikesSeeder
    {
        public static List<Likes> SeedLikes() 
        {
            return new List<Likes>
            {
                new Likes
                {
                    Id = Likes1.Id,
                    ArticleId = Likes1.ArticleId,
                    UserId = Likes1.UserId,
                    IpAddress = Likes1.IpAddress,
                    LikeCount = Likes1.LikeCount
                },
                new Likes
                {
                    Id = Likes2.Id,
                    ArticleId = Likes2.ArticleId,
                    UserId = Likes2.UserId,
                    IpAddress = Likes2.IpAddress,
                    LikeCount = Likes2.LikeCount
                },
                new Likes
                {
                    Id = Likes3.Id,
                    ArticleId = Likes3.ArticleId,
                    UserId = Likes3.UserId,
                    IpAddress = Likes3.IpAddress,
                    LikeCount = Likes3.LikeCount
                },
                new Likes
                {
                    Id = Likes4.Id,
                    ArticleId = Likes4.ArticleId,
                    UserId = Likes4.UserId,
                    IpAddress = Likes4.IpAddress,
                    LikeCount = Likes4.LikeCount
                }
            };
        }
    }
}
