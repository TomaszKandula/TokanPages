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
                    Id = Subscriber1.FId,
                    Email = Subscriber1.EMAIL,
                    IsActivated = Subscriber1.IS_ACTIVATED,
                    Count = Subscriber1.COUNT,
                    Registered = Subscriber1.FRegistered,
                    LastUpdated = Subscriber1.FLastUpdated
                },
                new ()
                {
                    Id = Subscriber2.FId,
                    Email = Subscriber2.EMAIL,
                    IsActivated = Subscriber2.IS_ACTIVATED,
                    Count = Subscriber2.COUNT,
                    Registered = Subscriber2.FRegistered,
                    LastUpdated = Subscriber2.FLastUpdated
                },
                new ()
                {
                    Id = Subscriber3.FId,
                    Email = Subscriber3.EMAIL,
                    IsActivated = Subscriber3.IS_ACTIVATED,
                    Count = Subscriber3.COUNT,
                    Registered = Subscriber3.FRegistered,
                    LastUpdated = Subscriber3.FLastUpdated
                }
            };
        }
    }
}