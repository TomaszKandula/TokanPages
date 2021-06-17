using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Initializer.Seeders
{
    [ExcludeFromCodeCoverage]
    public static class ArticlesSeeder
    {
        public static IEnumerable<Articles> SeedArticles()
        {
            return new List<Articles>
            {
                new ()
                {
                    Id = Data.Article1.FId,
                    Title = Data.Article1.TITLE,
                    Description = Data.Article1.DESCRIPTION,
                    IsPublished = Data.Article1.IS_PUBLISHED,
                    ReadCount = Data.Article1.READ_COUNT,
                    CreatedAt = Data.Article1.FCreated,
                    UpdatedAt = Data.Article1.FLastUpdated,
                    UserId = Data.Article1.FUserId
                },
                new ()
                {
                    Id = Data.Article2.FId,
                    Title = Data.Article2.TITLE,
                    Description = Data.Article2.DESCRIPTION,
                    IsPublished = Data.Article2.IS_PUBLISHED,
                    ReadCount = Data.Article2.READ_COUNT,
                    CreatedAt = Data.Article2.FCreated,
                    UpdatedAt = Data.Article2.FLastUpdated,
                    UserId = Data.Article2.FUserId
                },
                new ()
                {
                    Id = Data.Article3.FId,
                    Title = Data.Article3.TITLE,
                    Description = Data.Article3.DESCRIPTION,
                    IsPublished = Data.Article3.IS_PUBLISHED,
                    ReadCount = Data.Article3.READ_COUNT,
                    CreatedAt = Data.Article3.FCreated,
                    UpdatedAt = Data.Article3.FLastUpdated,
                    UserId = Data.Article3.FUserId
                },
                new ()
                {
                    Id = Data.Article4.FId,
                    Title = Data.Article4.TITLE,
                    Description = Data.Article4.DESCRIPTION,
                    IsPublished = Data.Article4.IS_PUBLISHED,
                    ReadCount = Data.Article4.READ_COUNT,
                    CreatedAt = Data.Article4.FCreated,
                    UpdatedAt = Data.Article4.FLastUpdated,
                    UserId = Data.Article4.FUserId
                }
            };
        }
    }
}
