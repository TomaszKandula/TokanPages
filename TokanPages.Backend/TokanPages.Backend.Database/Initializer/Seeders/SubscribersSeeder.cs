using System.Collections.Generic;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Initializer.Seeders
{
    public static class SubscribersSeeder
    {
        public static IEnumerable<Subscribers> SeedSubscribers()
        {
            return new List<Subscribers>
            {
                new ()
                {
                    Id = Data.Subscribers1.FId,
                    Email = Data.Subscribers1.EMAIL,
                    IsActivated = Data.Subscribers1.IS_ACTIVATED,
                    Count = Data.Subscribers1.COUNT,
                    Registered = Data.Subscribers1.FRegistered,
                    LastUpdated = Data.Subscribers1.FLastUpdated
                },
                new ()
                {
                    Id = Data.Subscribers2.FId,
                    Email = Data.Subscribers2.EMAIL,
                    IsActivated = Data.Subscribers2.IS_ACTIVATED,
                    Count = Data.Subscribers2.COUNT,
                    Registered = Data.Subscribers2.FRegistered,
                    LastUpdated = Data.Subscribers2.FLastUpdated
                },
                new ()
                {
                    Id = Data.Subscribers3.FId,
                    Email = Data.Subscribers3.EMAIL,
                    IsActivated = Data.Subscribers3.IS_ACTIVATED,
                    Count = Data.Subscribers3.COUNT,
                    Registered = Data.Subscribers3.FRegistered,
                    LastUpdated = Data.Subscribers3.FLastUpdated
                }
            };
        }
    }
}
