using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Seeders
{

    public class SubscribersSeeder : IDatabaseContextSeeder
    {

        public void Seed(ModelBuilder AModelBuilder)
        {

            AModelBuilder.Entity<Subscribers>()
                .HasData(
                    new Subscribers 
                    { 
                        Id = Dummies.Subscribers1.Id,
                        Email = Dummies.Subscribers1.Email,
                        IsActivated = Dummies.Subscribers1.IsActivated,
                        Count = Dummies.Subscribers1.Count,
                        Registered = Dummies.Subscribers1.Registered,
                        LastUpdated = Dummies.Subscribers1.LastUpdated
                    },
                    new Subscribers
                    {
                        Id = Dummies.Subscribers2.Id,
                        Email = Dummies.Subscribers2.Email,
                        IsActivated = Dummies.Subscribers2.IsActivated,
                        Count = Dummies.Subscribers2.Count,
                        Registered = Dummies.Subscribers2.Registered,
                        LastUpdated = Dummies.Subscribers2.LastUpdated
                    },
                    new Subscribers
                    {
                        Id = Dummies.Subscribers2.Id,
                        Email = Dummies.Subscribers2.Email,
                        IsActivated = Dummies.Subscribers2.IsActivated,
                        Count = Dummies.Subscribers2.Count,
                        Registered = Dummies.Subscribers2.Registered,
                        LastUpdated = Dummies.Subscribers2.LastUpdated
                    }
                );

        }

    }

}
