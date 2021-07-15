namespace TokanPages.Backend.Database.Initializer.Seeders
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Data.Articles;
    using Domain.Entities;

    [ExcludeFromCodeCoverage]
    public static class ArticlesSeeder
    {
        public static IEnumerable<Articles> SeedArticles()
        {
            return new List<Articles>
            {
                new ()
                {
                    Id = Article1.FId,
                    Title = Article1.TITLE,
                    Description = Article1.DESCRIPTION,
                    IsPublished = Article1.IS_PUBLISHED,
                    ReadCount = Article1.READ_COUNT,
                    CreatedAt = Article1.FCreated,
                    UpdatedAt = Article1.FLastUpdated,
                    UserId = Article1.FUserId
                },
                new ()
                {
                    Id = Article2.FId,
                    Title = Article2.TITLE,
                    Description = Article2.DESCRIPTION,
                    IsPublished = Article2.IS_PUBLISHED,
                    ReadCount = Article2.READ_COUNT,
                    CreatedAt = Article2.FCreated,
                    UpdatedAt = Article2.FLastUpdated,
                    UserId = Article2.FUserId
                },
                new ()
                {
                    Id = Article3.FId,
                    Title = Article3.TITLE,
                    Description = Article3.DESCRIPTION,
                    IsPublished = Article3.IS_PUBLISHED,
                    ReadCount = Article3.READ_COUNT,
                    CreatedAt = Article3.FCreated,
                    UpdatedAt = Article3.FLastUpdated,
                    UserId = Article3.FUserId
                },
                new ()
                {
                    Id = Article4.FId,
                    Title = Article4.TITLE,
                    Description = Article4.DESCRIPTION,
                    IsPublished = Article4.IS_PUBLISHED,
                    ReadCount = Article4.READ_COUNT,
                    CreatedAt = Article4.FCreated,
                    UpdatedAt = Article4.FLastUpdated,
                    UserId = Article4.FUserId
                }
            };
        }
    }
}