using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.Database.Initializer.Data.Subscribers;

namespace TokanPages.Persistence.MigrationRunner.DataSeeder.Seeders;

[ExcludeFromCodeCoverage]
public static class SubscribersSeeder
{
    public static IEnumerable<Subscribers> SeedSubscribers()
    {
        return new List<Subscribers>
        {
            new()
            {
                Id = Subscriber1.Id,
                Email = Subscriber1.Email,
                IsActivated = Subscriber1.IsActivated,
                Count = Subscriber1.Count,
                CreatedAt = Subscriber1.CreatedAt,
                CreatedBy = Subscriber1.CreatedBy,
                ModifiedAt = Subscriber1.ModifiedAt,
                ModifiedBy = Subscriber1.ModifiedBy
            },
            new()
            {
                Id = Subscriber2.Id,
                Email = Subscriber2.Email,
                IsActivated = Subscriber2.IsActivated,
                Count = Subscriber2.Count,
                CreatedAt = Subscriber2.CreatedAt,
                CreatedBy = Subscriber2.CreatedBy,
                ModifiedAt = Subscriber2.ModifiedAt,
                ModifiedBy = Subscriber2.ModifiedBy
            },
            new()
            {
                Id = Subscriber3.Id,
                Email = Subscriber3.Email,
                IsActivated = Subscriber3.IsActivated,
                Count = Subscriber3.Count,
                CreatedAt = Subscriber3.CreatedAt,
                CreatedBy = Subscriber3.CreatedBy,
                ModifiedAt = Subscriber3.ModifiedAt,
                ModifiedBy = Subscriber3.ModifiedBy
            }
        };
    }
}