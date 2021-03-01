using System.Collections.Generic;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Seeders
{
    public class ArticlesSeeder
    {
        public static List<Articles> SeedArticles()
        {
            return new List<Articles>
            {
                new Articles
                {
                    Id = Dummies.Article1.Id,
                    Title = Dummies.Article1.Title,
                    Description = Dummies.Article1.Description,
                    IsPublished = Dummies.Article1.IsPublished,
                    ReadCount = Dummies.Article1.ReadCount,
                    CreatedAt = Dummies.Article1.Created,
                    UpdatedAt = Dummies.Article1.LastUpdated,
                    UserId = Dummies.Article1.UserId
                },
                new Articles
                {
                    Id = Dummies.Article2.Id,
                    Title = Dummies.Article2.Title,
                    Description = Dummies.Article2.Description,
                    IsPublished = Dummies.Article2.IsPublished,
                    ReadCount = Dummies.Article2.ReadCount,
                    CreatedAt = Dummies.Article2.Created,
                    UpdatedAt = Dummies.Article2.LastUpdated,
                    UserId = Dummies.Article2.UserId
                },
                new Articles
                {
                    Id = Dummies.Article3.Id,
                    Title = Dummies.Article3.Title,
                    Description = Dummies.Article3.Description,
                    IsPublished = Dummies.Article3.IsPublished,
                    ReadCount = Dummies.Article3.ReadCount,
                    CreatedAt = Dummies.Article3.Created,
                    UpdatedAt = Dummies.Article3.LastUpdated,
                    UserId = Dummies.Article3.UserId
                },
                new Articles
                {
                    Id = Dummies.Article4.Id,
                    Title = Dummies.Article4.Title,
                    Description = Dummies.Article4.Description,
                    IsPublished = Dummies.Article4.IsPublished,
                    ReadCount = Dummies.Article4.ReadCount,
                    CreatedAt = Dummies.Article4.Created,
                    UpdatedAt = Dummies.Article4.LastUpdated,
                    UserId = Dummies.Article4.UserId
                }
            };
        }
    }
}
