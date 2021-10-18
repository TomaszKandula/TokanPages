namespace TokanPages.Backend.Database.Initializer.Seeders
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Domain.Entities;
    using Data.Subscribers;

    [ExcludeFromCodeCoverage]
    public static class SubscribersSeeder
    {
        public static IEnumerable<Subscribers> SeedSubscribers()
        {
            return new List<Subscribers>
            {
                new ()
                {
                    Id = Subscriber1.Id,
                    Email = Subscriber1.Email,
                    IsActivated = Subscriber1.IsActivated,
                    Count = Subscriber1.Count,
                    Registered = Subscriber1.Registered,
                    LastUpdated = Subscriber1.LastUpdated
                },
                new ()
                {
                    Id = Subscriber2.Id,
                    Email = Subscriber2.Email,
                    IsActivated = Subscriber2.IsActivated,
                    Count = Subscriber2.Count,
                    Registered = Subscriber2.Registered,
                    LastUpdated = Subscriber2.LastUpdated
                },
                new ()
                {
                    Id = Subscriber3.Id,
                    Email = Subscriber3.Email,
                    IsActivated = Subscriber3.IsActivated,
                    Count = Subscriber3.Count,
                    Registered = Subscriber3.Registered,
                    LastUpdated = Subscriber3.LastUpdated
                }
            };
        }
    }
}