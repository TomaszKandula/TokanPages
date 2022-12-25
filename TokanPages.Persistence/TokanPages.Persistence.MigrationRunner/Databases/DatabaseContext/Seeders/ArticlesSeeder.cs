using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.Database.Initializer.Data.Articles;

namespace TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Seeders;

[ExcludeFromCodeCoverage]
public static class ArticlesSeeder
{
    public static IEnumerable<Articles> SeedArticles()
    {
        return new List<Articles>
        {
            new()
            {
                Id = Article1.Id,
                Title = Article1.Title,
                Description = Article1.Description,
                IsPublished = Article1.IsPublished,
                ReadCount = Article1.ReadCount,
                CreatedAt = Article1.CreatedAt,
                UpdatedAt = Article1.UpdatedAt,
                UserId = Article1.UserId
            },
            new()
            {
                Id = Article2.Id,
                Title = Article2.Title,
                Description = Article2.Description,
                IsPublished = Article2.IsPublished,
                ReadCount = Article2.ReadCount,
                CreatedAt = Article2.CreatedAt,
                UpdatedAt = Article2.UpdatedAt,
                UserId = Article2.UserId
            },
            new()
            {
                Id = Article3.Id,
                Title = Article3.Title,
                Description = Article3.Description,
                IsPublished = Article3.IsPublished,
                ReadCount = Article3.ReadCount,
                CreatedAt = Article3.CreatedAt,
                UpdatedAt = Article3.UpdatedAt,
                UserId = Article3.UserId
            },
            new()
            {
                Id = Article4.Id,
                Title = Article4.Title,
                Description = Article4.Description,
                IsPublished = Article4.IsPublished,
                ReadCount = Article4.ReadCount,
                CreatedAt = Article4.CreatedAt,
                UpdatedAt = Article4.UpdatedAt,
                UserId = Article4.UserId
            },
            new()
            {
                Id = Article5.Id,
                Title = Article5.Title,
                Description = Article5.Description,
                IsPublished = Article5.IsPublished,
                ReadCount = Article5.ReadCount,
                CreatedAt = Article5.Created,
                UpdatedAt = Article5.LastUpdated,
                UserId = Article5.UserId
            }
        };
    }
}