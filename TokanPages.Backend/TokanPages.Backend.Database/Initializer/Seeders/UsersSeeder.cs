using System.Collections.Generic;
using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Backend.Database.Initializer.Seeders
{
    public static class UsersSeeder
    {       
        public static IEnumerable<Users> SeedUsers()
        {
            return new List<Users>
            {
                new ()
                {
                    Id = Data.User1.FId,
                    EmailAddress = Data.User1.EMAIL_ADDRESS,
                    IsActivated = Data.User1.IS_ACTIVATED,
                    UserAlias = Data.User1.USER_ALIAS,
                    FirstName = Data.User1.FIRST_NAME,
                    LastName = Data.User1.LAST_NAME,
                    Registered = Data.User1.FRegistered,
                    LastLogged = Data.User1.FLastLogged,
                    LastUpdated = Data.User1.FLastUpdated,
                    AvatarName = Data.User1.AVATAR_NAME,
                    ShortBio = Data.User1.SHORT_BIO
                },
                new ()
                {
                    Id = Data.User2.FId,
                    EmailAddress = Data.User2.EMAIL_ADDRESS,
                    IsActivated = Data.User2.IS_ACTIVATED,
                    UserAlias = Data.User2.USER_ALIAS,
                    FirstName = Data.User2.FIRST_NAME,
                    LastName = Data.User2.LAST_NAME,
                    Registered = Data.User2.FRegistered,
                    LastLogged = Data.User2.FLastLogged,
                    LastUpdated = Data.User2.FLastUpdated,
                    AvatarName = Data.User2.AVATAR_NAME,
                    ShortBio = Data.User2.SHORT_BIO
                },
                new ()
                {
                    Id = Data.User3.FId,
                    EmailAddress = Data.User3.EMAIL_ADDRESS,
                    IsActivated = Data.User3.IS_ACTIVATED,
                    UserAlias = Data.User3.USER_ALIAS,
                    FirstName = Data.User3.FIRST_NAME,
                    LastName = Data.User3.LAST_NAME,
                    Registered = Data.User3.FRegistered,
                    LastLogged = Data.User3.FLastLogged,
                    LastUpdated = Data.User3.FLastUpdated,
                    AvatarName = Data.User3.AVATAR_NAME,
                    ShortBio = Data.User3.SHORT_BIO
                }
            };
        }
    }
}
