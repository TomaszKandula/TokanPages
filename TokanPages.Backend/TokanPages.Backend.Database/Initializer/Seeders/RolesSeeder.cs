using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Database.Initializer.Data.Roles;

namespace TokanPages.Backend.Database.Initializer.Seeders
{
    [ExcludeFromCodeCoverage]
    public static class RolesSeeder
    {
        public static IEnumerable<Roles> SeedRoles()
        {
            return new List<Roles>
            {
                new ()
                {
                    Id = Role1.FId,
                    Name = Role1.FName,
                    Description = Role1.DESCRIPTION
                },
                new ()
                {
                    Id = Role2.FId,
                    Name = Role2.FName,
                    Description = Role2.DESCRIPTION
                },
                new ()
                {
                    Id = Role3.FId,
                    Name = Role3.FName,
                    Description = Role3.DESCRIPTION
                },
                new ()
                {
                    Id = Role4.FId,
                    Name = Role4.FName,
                    Description = Role4.DESCRIPTION
                },
                new ()
                {
                    Id = Role5.FId,
                    Name = Role5.FName,
                    Description = Role5.DESCRIPTION
                },
            };
        }
    }
}