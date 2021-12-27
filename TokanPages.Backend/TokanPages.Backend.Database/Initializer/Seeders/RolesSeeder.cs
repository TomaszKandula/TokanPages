namespace TokanPages.Backend.Database.Initializer.Seeders;

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
            new ()
            {
                Id = Role1.Id,
                Name = Role1.Name,
                Description = Role1.Description
            },
            new ()
            {
                Id = Role2.Id,
                Name = Role2.Name,
                Description = Role2.Description
            },
            new ()
            {
                Id = Role3.Id,
                Name = Role3.Name,
                Description = Role3.Description
            },
            new ()
            {
                Id = Role4.Id,
                Name = Role4.Name,
                Description = Role4.Description
            },
            new ()
            {
                Id = Role5.Id,
                Name = Role5.Name,
                Description = Role5.Description
            },
        };
    }
}