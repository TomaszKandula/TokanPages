using System.Collections.Generic;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Seeders
{
    public static class UsersSeeder
    {       
        public static IEnumerable<Users> SeedUsers()
        {
            return new List<Users>
            {
                new Users
                {
                    Id = Dummies.User1.FId,
                    EmailAddress = Dummies.User1.EMAIL_ADDRESS,
                    IsActivated = Dummies.User1.IS_ACTIVATED,
                    UserAlias = Dummies.User1.USER_ALIAS,
                    FirstName = Dummies.User1.FIRST_NAME,
                    LastName = Dummies.User1.LAST_NAME,
                    Registered = Dummies.User1.FRegistered,
                    LastLogged = Dummies.User1.FLastLogged,
                    LastUpdated = Dummies.User1.FLastUpdated,
                    AvatarName = Dummies.User1.AVATAR_NAME,
                    ShortBio = Dummies.User1.SHORT_BIO
                },
                new Users
                {
                    Id = Dummies.User2.FId,
                    EmailAddress = Dummies.User2.EMAIL_ADDRESS,
                    IsActivated = Dummies.User2.IS_ACTIVATED,
                    UserAlias = Dummies.User2.USER_ALIAS,
                    FirstName = Dummies.User2.FIRST_NAME,
                    LastName = Dummies.User2.LAST_NAME,
                    Registered = Dummies.User2.FRegistered,
                    LastLogged = Dummies.User2.FLastLogged,
                    LastUpdated = Dummies.User2.FLastUpdated,
                    AvatarName = Dummies.User2.AVATAR_NAME,
                    ShortBio = Dummies.User2.SHORT_BIO
                },
                new Users
                {
                    Id = Dummies.User3.FId,
                    EmailAddress = Dummies.User3.EMAIL_ADDRESS,
                    IsActivated = Dummies.User3.IS_ACTIVATED,
                    UserAlias = Dummies.User3.USER_ALIAS,
                    FirstName = Dummies.User3.FIRST_NAME,
                    LastName = Dummies.User3.LAST_NAME,
                    Registered = Dummies.User3.FRegistered,
                    LastLogged = Dummies.User3.FLastLogged,
                    LastUpdated = Dummies.User3.FLastUpdated,
                    AvatarName = Dummies.User3.AVATAR_NAME,
                    ShortBio = Dummies.User3.SHORT_BIO
                }
            };
        }
    }
}
