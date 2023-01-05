using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.UserRoles;

namespace TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Seeders;

[ExcludeFromCodeCoverage]
public static class UserRolesSeeder
{
    public static IEnumerable<UserRoles> SeedUserRoles()
    {
        return new List<UserRoles>
        {
            new()
            {
                Id = UserRole1.Id,
                UserId = UserRole1.UserId,
                RoleId = UserRole1.RoleId,
                CreatedAt = UserRole1.CreatedAt,
                CreatedBy = UserRole1.CreatedBy,
                ModifiedAt = UserRole1.ModifiedAt,
                ModifiedBy = UserRole1.ModifiedBy
            },
            new()
            {
                Id = UserRole2.Id,
                UserId = UserRole2.UserId,
                RoleId = UserRole2.RoleId,
                CreatedAt = UserRole2.CreatedAt,
                CreatedBy = UserRole2.CreatedBy,
                ModifiedAt = UserRole2.ModifiedAt,
                ModifiedBy = UserRole2.ModifiedBy
            },
            new()
            {
                Id = UserRole3.Id,
                UserId = UserRole3.UserId,
                RoleId = UserRole3.RoleId,
                CreatedAt = UserRole3.CreatedAt,
                CreatedBy = UserRole3.CreatedBy,
                ModifiedAt = UserRole3.ModifiedAt,
                ModifiedBy = UserRole3.ModifiedBy
            },
            new()
            {
                Id = UserRole4.Id,
                UserId = UserRole4.UserId,
                RoleId = UserRole4.RoleId,
                CreatedAt = UserRole4.CreatedAt,
                CreatedBy = UserRole4.CreatedBy,
                ModifiedAt = UserRole4.ModifiedAt,
                ModifiedBy = UserRole4.ModifiedBy
            },
            new()
            {
                Id = UserRole5.Id,
                UserId = UserRole5.UserId,
                RoleId = UserRole5.RoleId,
                CreatedAt = UserRole5.CreatedAt,
                CreatedBy = UserRole5.CreatedBy,
                ModifiedAt = UserRole5.ModifiedAt,
                ModifiedBy = UserRole5.ModifiedBy
            }
        };
    }
}