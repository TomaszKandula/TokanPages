using System.Collections.Generic;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Seeders
{
    public class UsersSeeder
    {       
        public static List<Users> SeedUsers()
        {
            return new List<Users>
            {
                new Users
                {
                    Id = Dummies.User1.Id,
                    EmailAddress = Dummies.User1.EmailAddress,
                    IsActivated = Dummies.User1.IsActivated,
                    UserAlias = Dummies.User1.UserAlias,
                    FirstName = Dummies.User1.FirstName,
                    LastName = Dummies.User1.LastName,
                    Registered = Dummies.User1.Registered,
                    LastLogged = Dummies.User1.LastLogged,
                    LastUpdated = Dummies.User1.LastUpdated
                },
                new Users
                {
                    Id = Dummies.User2.Id,
                    EmailAddress = Dummies.User2.EmailAddress,
                    IsActivated = Dummies.User2.IsActivated,
                    UserAlias = Dummies.User2.UserAlias,
                    FirstName = Dummies.User2.FirstName,
                    LastName = Dummies.User2.LastName,
                    Registered = Dummies.User2.Registered,
                    LastLogged = Dummies.User2.LastLogged,
                    LastUpdated = Dummies.User2.LastUpdated
                },
                new Users
                {
                    Id = Dummies.User3.Id,
                    EmailAddress = Dummies.User3.EmailAddress,
                    IsActivated = Dummies.User3.IsActivated,
                    UserAlias = Dummies.User3.UserAlias,
                    FirstName = Dummies.User3.FirstName,
                    LastName = Dummies.User3.LastName,
                    Registered = Dummies.User3.Registered,
                    LastLogged = Dummies.User3.LastLogged,
                    LastUpdated = Dummies.User3.LastUpdated
                }
            };
        }
    }
}
