using System;
using System.Collections.Generic;
using TokanPages.Backend.Domain.Entities;

namespace Backend.UnitTests.FakeDatabase
{

    public static class DummyLoad
    {

        public static List<Articles> GetArticles() 
        {
            return new List<Articles>
            {
                new Articles
                { 
                    Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                    Title = "Why C# is great?",
                    Description = "More on C#",
                    IsPublished = false,
                    Likes = 0,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                },
                new Articles
                {
                    Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                    Title = "NET Core 5 is coming",
                    Description = "What's new?",
                    IsPublished = false,
                    Likes = 0,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                }
            };
        }

        public static List<Subscribers> GetSubscribers()
        {
            return new List<Subscribers> 
            { 
                new Subscribers
                { 
                    Id = Guid.Parse("e6c50982-2fb3-481b-bdb3-5ca3deec5a13"),
                    Count = 0,
                    Email = "ester.exposito@gmail.com",
                    Registered = DateTime.Now,
                    IsActivated = true,
                    LastUpdated = null
                },
                new Subscribers
                {
                    Id = Guid.Parse("eaa8bb69-719b-46cf-ae23-3b60e1f61621"),
                    Count = 0,
                    Email = "tokan@dfds.com",
                    Registered = DateTime.Now,
                    IsActivated = true,
                    LastUpdated = null
                }
            };
        }

        public static List<Users> GetUsers()
        {
            return new List<Users>
            {
                new Users
                { 
                    Id = Guid.Parse("66d9aeb9-95b7-48c8-84ad-31ee32e2426b"),
                    EmailAddress = "ester.exposito@gmail.com",
                    UserAlias = "Estera",
                    FirstName = "Ester",
                    LastName = "Exposito",
                    IsActivated = true,
                    Registered = DateTime.Now,
                    LastLogged = DateTime.Now,
                    LastUpdated = null
                }, 
                new Users
                {
                    Id = Guid.Parse("fb971c5a-70c6-44b9-ba39-0a92e28a85dd"),
                    EmailAddress = "tokan@dfds.com",
                    UserAlias = "Tokan",
                    FirstName = "Tomasz",
                    LastName = "Kandula",
                    IsActivated = true,
                    Registered = DateTime.Now,
                    LastLogged = DateTime.Now,
                    LastUpdated = null
                }
            };
        }

    }

}
