namespace TokanPages.Backend.Database.Initializer.Seeders;

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
                Id = User1.Id,
                EmailAddress = User1.EmailAddress,
                IsActivated = User1.IsActivated,
                UserAlias = User1.UserAlias,
                FirstName = User1.FirstName,
                LastName = User1.LastName,
                Registered = User1.Registered,
                LastLogged = User1.LastLogged,
                LastUpdated = User1.LastUpdated,
                AvatarName = User1.AvatarName,
                ShortBio = User1.ShortBio,
                CryptedPassword = User1.CryptedPassword,
                ResetId = User1.ResetId
            },
            new ()
            {
                Id = User2.Id,
                EmailAddress = User2.EmailAddress,
                IsActivated = User2.IsActivated,
                UserAlias = User2.UserAlias,
                FirstName = User2.FirstName,
                LastName = User2.LastName,
                Registered = User2.Registered,
                LastLogged = User2.LastLogged,
                LastUpdated = User2.LastUpdated,
                AvatarName = User2.AvatarName,
                ShortBio = User2.ShortBio,
                CryptedPassword = User2.CryptedPassword,
                ResetId = User2.ResetId
            },
            new ()
            {
                Id = User3.Id,
                EmailAddress = User3.EmailAddress,
                IsActivated = User3.IsActivated,
                UserAlias = User3.UserAlias,
                FirstName = User3.FirstName,
                LastName = User3.LastName,
                Registered = User3.Registered,
                LastLogged = User3.LastLogged,
                LastUpdated = User3.LastUpdated,
                AvatarName = User3.AvatarName,
                ShortBio = User3.ShortBio,
                CryptedPassword = User3.CryptedPassword,
                ResetId = User3.ResetId
            }
        };
    }
}