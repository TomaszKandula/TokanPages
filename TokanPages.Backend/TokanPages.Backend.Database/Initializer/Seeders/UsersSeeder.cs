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
                LastLogged = User1.LastLogged,
                CryptedPassword = User1.CryptedPassword,
                ResetId = User1.ResetId
            },
            new ()
            {
                Id = User2.Id,
                EmailAddress = User2.EmailAddress,
                IsActivated = User2.IsActivated,
                UserAlias = User2.UserAlias,
                LastLogged = User2.LastLogged,
                CryptedPassword = User2.CryptedPassword,
                ResetId = User2.ResetId
            },
            new ()
            {
                Id = User3.Id,
                EmailAddress = User3.EmailAddress,
                IsActivated = User3.IsActivated,
                UserAlias = User3.UserAlias,
                LastLogged = User3.LastLogged,
                CryptedPassword = User3.CryptedPassword,
                ResetId = User3.ResetId
            }
        };
    }
}