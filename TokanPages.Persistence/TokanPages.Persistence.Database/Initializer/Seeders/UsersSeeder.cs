using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.Database.Initializer.Data.Users;

namespace TokanPages.Persistence.Database.Initializer.Seeders;

[ExcludeFromCodeCoverage]
public static class UsersSeeder
{       
    public static IEnumerable<Users> SeedUsers()
    {
        return new List<Users>
        {
            new()
            {
                Id = User1.Id,
                UserAlias = User1.UserAlias,
                EmailAddress = User1.EmailAddress,
                CryptedPassword = User1.CryptedPassword,
                ResetId = User1.ResetId,
                CreatedBy = User1.CreatedBy,
                CreatedAt = User1.CreatedAt,
                ModifiedBy = User1.ModifiedBy,
                ModifiedAt = User1.ModifiedAt,
                IsActivated = User1.IsActivated,
                IsDeleted = User1.IsDeleted
            },
            new()
            {
                Id = User2.Id,
                EmailAddress = User2.EmailAddress,
                UserAlias = User2.UserAlias,
                CryptedPassword = User2.CryptedPassword,
                ResetId = User2.ResetId,
                CreatedBy = User2.CreatedBy,
                CreatedAt = User2.CreatedAt,
                ModifiedBy = User2.ModifiedBy,
                ModifiedAt = User2.ModifiedAt,
                IsActivated = User2.IsActivated,
                IsDeleted = User2.IsDeleted
            },
            new()
            {
                Id = User3.Id,
                EmailAddress = User3.EmailAddress,
                UserAlias = User3.UserAlias,
                CryptedPassword = User3.CryptedPassword,
                ResetId = User3.ResetId,
                CreatedBy = User3.CreatedBy,
                CreatedAt = User3.CreatedAt,
                ModifiedBy = User3.ModifiedBy,
                ModifiedAt = User3.ModifiedAt,
                IsActivated = User3.IsActivated,
                IsDeleted = User3.IsDeleted
            }
        };
    }
}