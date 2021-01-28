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
                    UpdatedAt = Dummies.Article1.LastUpdated
                },
                new Articles
                {
                    Id = Dummies.Article2.Id,
                    Title = Dummies.Article2.Title,
                    Description = Dummies.Article2.Description,
                    IsPublished = Dummies.Article2.IsPublished,
                    ReadCount = Dummies.Article2.ReadCount,
                    CreatedAt = Dummies.Article2.Created,
                    UpdatedAt = Dummies.Article2.LastUpdated
                },
                new Articles
                {
                    Id = Dummies.Article3.Id,
                    Title = Dummies.Article3.Title,
                    Description = Dummies.Article3.Description,
                    IsPublished = Dummies.Article3.IsPublished,
                    ReadCount = Dummies.Article3.ReadCount,
                    CreatedAt = Dummies.Article3.Created,
                    UpdatedAt = Dummies.Article3.LastUpdated
                }
            };
        }
    }
}
