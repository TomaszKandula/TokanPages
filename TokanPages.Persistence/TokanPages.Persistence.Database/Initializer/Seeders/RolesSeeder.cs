using TokanPages.Backend.Domain.Entities;

namespace TokanPages.Persistence.Database.Initializer.Seeders;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Data.Roles;
using Domain.Entities;

[ExcludeFromCodeCoverage]
public static class RolesSeeder
{
    public static IEnumerable<Roles> SeedRoles()
    {
        return new List<Roles>
        {
            new()
            {
                Id = Role1.Id,
                Name = Role1.Name,
                Description = Role1.Description,
                CreatedAt = Role1.CreatedAt,
                CreatedBy = Role1.CreatedBy,
                ModifiedAt = Role1.ModifiedAt,
                ModifiedBy = Role1.ModifiedBy
            },
            new()
            {
                Id = Role2.Id,
                Name = Role2.Name,
                Description = Role2.Description,
                CreatedAt = Role2.CreatedAt,
                CreatedBy = Role2.CreatedBy,
                ModifiedAt = Role2.ModifiedAt,
                ModifiedBy = Role2.ModifiedBy
            },
            new()
            {
                Id = Role3.Id,
                Name = Role3.Name,
                Description = Role3.Description,
                CreatedAt = Role3.CreatedAt,
                CreatedBy = Role3.CreatedBy,
                ModifiedAt = Role3.ModifiedAt,
                ModifiedBy = Role3.ModifiedBy
            },
            new()
            {
                Id = Role4.Id,
                Name = Role4.Name,
                Description = Role4.Description,
                CreatedAt = Role4.CreatedAt,
                CreatedBy = Role4.CreatedBy,
                ModifiedAt = Role4.ModifiedAt,
                ModifiedBy = Role4.ModifiedBy
            },
            new()
            {
                Id = Role5.Id,
                Name = Role5.Name,
                Description = Role5.Description,
                CreatedAt = Role5.CreatedAt,
                CreatedBy = Role5.CreatedBy,
                ModifiedAt = Role5.ModifiedAt,
                ModifiedBy = Role5.ModifiedBy
            },
        };
    }
}