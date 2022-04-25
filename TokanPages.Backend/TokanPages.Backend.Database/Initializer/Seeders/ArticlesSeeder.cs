namespace TokanPages.Backend.Database.Initializer.Seeders;

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
                Id = Article1.Id,
                Title = Article1.Title,
                Description = Article1.Description,
                IsPublished = Article1.IsPublished,
                ReadCount = Article1.ReadCount,
                CreatedAt = Article1.Created,
                UpdatedAt = Article1.LastUpdated,
                UserId = Article1.UserId
            },
            new ()
            {
                Id = Article2.Id,
                Title = Article2.Title,
                Description = Article2.Description,
                IsPublished = Article2.IsPublished,
                ReadCount = Article2.ReadCount,
                CreatedAt = Article2.Created,
                UpdatedAt = Article2.LastUpdated,
                UserId = Article2.UserId
            },
            new ()
            {
                Id = Article3.Id,
                Title = Article3.Title,
                Description = Article3.Description,
                IsPublished = Article3.IsPublished,
                ReadCount = Article3.ReadCount,
                CreatedAt = Article3.Created,
                UpdatedAt = Article3.LastUpdated,
                UserId = Article3.UserId
            },
            new ()
            {
                Id = Article4.Id,
                Title = Article4.Title,
                Description = Article4.Description,
                IsPublished = Article4.IsPublished,
                ReadCount = Article4.ReadCount,
                CreatedAt = Article4.Created,
                UpdatedAt = Article4.LastUpdated,
                UserId = Article4.UserId
            },
            new ()
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