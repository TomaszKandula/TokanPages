namespace TokanPages.Backend.Database.Initializer.Seeders
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Domain.Entities;
    using Data.Users;

    [ExcludeFromCodeCoverage]
    public static class UsersSeeder
    {       
        public static IEnumerable<Users> SeedUsers()
        {
            return new List<Users>
            {
                new ()
                {
                    Id = User1.FId,
                    EmailAddress = User1.EMAIL_ADDRESS,
                    IsActivated = User1.IS_ACTIVATED,
                    UserAlias = User1.USER_ALIAS,
                    FirstName = User1.FIRST_NAME,
                    LastName = User1.LAST_NAME,
                    Registered = User1.FRegistered,
                    LastLogged = User1.FLastLogged,
                    LastUpdated = User1.FLastUpdated,
                    AvatarName = User1.AVATAR_NAME,
                    ShortBio = User1.SHORT_BIO,
                    CryptedPassword = User1.CRYPTED_PASSWORD,
                    ResetId = User1.FResetId
                },
                new ()
                {
                    Id = User2.FId,
                    EmailAddress = User2.EMAIL_ADDRESS,
                    IsActivated = User2.IS_ACTIVATED,
                    UserAlias = User2.USER_ALIAS,
                    FirstName = User2.FIRST_NAME,
                    LastName = User2.LAST_NAME,
                    Registered = User2.FRegistered,
                    LastLogged = User2.FLastLogged,
                    LastUpdated = User2.FLastUpdated,
                    AvatarName = User2.AVATAR_NAME,
                    ShortBio = User2.SHORT_BIO,
                    CryptedPassword = User2.CRYPTED_PASSWORD,
                    ResetId = User2.FResetId
                },
                new ()
                {
                    Id = User3.FId,
                    EmailAddress = User3.EMAIL_ADDRESS,
                    IsActivated = User3.IS_ACTIVATED,
                    UserAlias = User3.USER_ALIAS,
                    FirstName = User3.FIRST_NAME,
                    LastName = User3.LAST_NAME,
                    Registered = User3.FRegistered,
                    LastLogged = User3.FLastLogged,
                    LastUpdated = User3.FLastUpdated,
                    AvatarName = User3.AVATAR_NAME,
                    ShortBio = User3.SHORT_BIO,
                    CryptedPassword = User3.CRYPTED_PASSWORD,
                    ResetId = User3.FResetId
                }
            };
        }
    }
}