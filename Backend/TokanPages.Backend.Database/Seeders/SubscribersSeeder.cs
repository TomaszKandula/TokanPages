using System.Collections.Generic;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Seeders
{
    public static class SubscribersSeeder
    {
        public static IEnumerable<Subscribers> SeedSubscribers()
        {
            return new List<Subscribers>
            {
                new Subscribers
                {
                    Id = Dummies.Subscribers1.FId,
                    Email = Dummies.Subscribers1.EMAIL,
                    IsActivated = Dummies.Subscribers1.IS_ACTIVATED,
                    Count = Dummies.Subscribers1.COUNT,
                    Registered = Dummies.Subscribers1.FRegistered,
                    LastUpdated = Dummies.Subscribers1.FLastUpdated
                },
                new Subscribers
                {
                    Id = Dummies.Subscribers2.FId,
                    Email = Dummies.Subscribers2.EMAIL,
                    IsActivated = Dummies.Subscribers2.IS_ACTIVATED,
                    Count = Dummies.Subscribers2.COUNT,
                    Registered = Dummies.Subscribers2.FRegistered,
                    LastUpdated = Dummies.Subscribers2.FLastUpdated
                },
                new Subscribers
                {
                    Id = Dummies.Subscribers3.FId,
                    Email = Dummies.Subscribers3.EMAIL,
                    IsActivated = Dummies.Subscribers3.IS_ACTIVATED,
                    Count = Dummies.Subscribers3.COUNT,
                    Registered = Dummies.Subscribers3.FRegistered,
                    LastUpdated = Dummies.Subscribers3.FLastUpdated
                }
            };
        }
    }
}
