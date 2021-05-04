using System.Collections.Generic;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Seeders
{
    public static class ArticlesSeeder
    {
        public static IEnumerable<Articles> SeedArticles()
        {
            return new List<Articles>
            {
                new Articles
                {
                    Id = Dummies.Article1.FId,
                    Title = Dummies.Article1.TITLE,
                    Description = Dummies.Article1.DESCRIPTION,
                    IsPublished = Dummies.Article1.IS_PUBLISHED,
                    ReadCount = Dummies.Article1.READ_COUNT,
                    CreatedAt = Dummies.Article1.FCreated,
                    UpdatedAt = Dummies.Article1.FLastUpdated,
                    UserId = Dummies.Article1.FUserId
                },
                new Articles
                {
                    Id = Dummies.Article2.FId,
                    Title = Dummies.Article2.TITLE,
                    Description = Dummies.Article2.DESCRIPTION,
                    IsPublished = Dummies.Article2.IS_PUBLISHED,
                    ReadCount = Dummies.Article2.READ_COUNT,
                    CreatedAt = Dummies.Article2.FCreated,
                    UpdatedAt = Dummies.Article2.FLastUpdated,
                    UserId = Dummies.Article2.FUserId
                },
                new Articles
                {
                    Id = Dummies.Article3.FId,
                    Title = Dummies.Article3.TITLE,
                    Description = Dummies.Article3.DESCRIPTION,
                    IsPublished = Dummies.Article3.IS_PUBLISHED,
                    ReadCount = Dummies.Article3.READ_COUNT,
                    CreatedAt = Dummies.Article3.FCreated,
                    UpdatedAt = Dummies.Article3.FLastUpdated,
                    UserId = Dummies.Article3.FUserId
                },
                new Articles
                {
                    Id = Dummies.Article4.FId,
                    Title = Dummies.Article4.TITLE,
                    Description = Dummies.Article4.DESCRIPTION,
                    IsPublished = Dummies.Article4.IS_PUBLISHED,
                    ReadCount = Dummies.Article4.READ_COUNT,
                    CreatedAt = Dummies.Article4.FCreated,
                    UpdatedAt = Dummies.Article4.FLastUpdated,
                    UserId = Dummies.Article4.FUserId
                }
            };
        }
    }
}
