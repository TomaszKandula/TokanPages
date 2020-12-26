using System;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Seeders
{

    public class SubscribersSeeder : IDatabaseContextSeeder
    {

        public class Dummy1 
        {
            public static Guid Id = Guid.Parse("098a9c38-c31d-4a29-b5a7-5d02a1a1f7ae");
            public static string Email = "ester1990@gmail.com";
            public static DateTime Registered = DateTime.Parse("2020-01-10 12:15:15");
            public static DateTime? LastUpdated = null;
            public static bool IsActivated = false;
            public static int Count = 0;
        }

        public class Dummy2 
        {
            public static Guid Id = Guid.Parse("ec8dd29c-464c-4e7a-897c-ce0ace2619ec");
            public static string Email = "tokan@dfds.com";
            public static DateTime Registered = DateTime.Parse("2020-01-25 05:09:19");
            public static DateTime? LastUpdated = null;
            public static bool IsActivated = false;
            public static int Count = 0;
        }

        public class Dummy3 
        {
            public static Guid Id = Guid.Parse("8a40f1b0-f983-4e51-9bfe-aeb5a5aee1bf");
            public static string Email = "admin@tomkandula.com";
            public static DateTime Registered = DateTime.Parse("2020-09-12 22:01:33");
            public static DateTime? LastUpdated = null;
            public static bool IsActivated = false;
            public static int Count = 0;
        }

        public void Seed(ModelBuilder AModelBuilder)
        {

            AModelBuilder.Entity<Subscribers>()
                .HasData(
                    new Subscribers 
                    { 
                        Id = Dummy1.Id,
                        Email = Dummy1.Email,
                        IsActivated = Dummy1.IsActivated,
                        Count = Dummy1.Count,
                        Registered = Dummy1.Registered,
                        LastUpdated = Dummy1.LastUpdated
                    },
                    new Subscribers
                    {
                        Id = Dummy2.Id,
                        Email = Dummy2.Email,
                        IsActivated = Dummy2.IsActivated,
                        Count = Dummy2.Count,
                        Registered = Dummy2.Registered,
                        LastUpdated = Dummy2.LastUpdated
                    },
                    new Subscribers
                    {
                        Id = Dummy3.Id,
                        Email = Dummy3.Email,
                        IsActivated = Dummy3.IsActivated,
                        Count = Dummy3.Count,
                        Registered = Dummy3.Registered,
                        LastUpdated = Dummy3.LastUpdated
                    }
                );

        }

    }

}
