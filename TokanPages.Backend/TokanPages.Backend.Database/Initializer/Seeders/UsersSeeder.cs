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
                CryptedPassword = User1.CryptedPassword,
                ResetId = User1.ResetId,
                CreatedBy = User1.CreatedBy,
                CreatedAt = User1.CreatedAt,
                ModifiedBy = User1.ModifiedBy,
                ModifiedAt = User1.ModifiedAt
            },
            new ()
            {
                Id = User2.Id,
                EmailAddress = User2.EmailAddress,
                IsActivated = User2.IsActivated,
                UserAlias = User2.UserAlias,
                CryptedPassword = User2.CryptedPassword,
                ResetId = User2.ResetId,
                CreatedBy = User2.CreatedBy,
                CreatedAt = User2.CreatedAt,
                ModifiedBy = User2.ModifiedBy,
                ModifiedAt = User2.ModifiedAt
            },
            new ()
            {
                Id = User3.Id,
                EmailAddress = User3.EmailAddress,
                IsActivated = User3.IsActivated,
                UserAlias = User3.UserAlias,
                CryptedPassword = User3.CryptedPassword,
                ResetId = User3.ResetId,
                CreatedBy = User3.CreatedBy,
                CreatedAt = User3.CreatedAt,
                ModifiedBy = User3.ModifiedBy,
                ModifiedAt = User3.ModifiedAt
            }
        };
    }
}