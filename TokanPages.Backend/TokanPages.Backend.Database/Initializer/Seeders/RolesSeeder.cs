using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Database.Initializer.Data;

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
                    Name = Role1.FName,
                    Description = Role1.DESCRIPTION
                },
                new ()
                {
                    Name = Role2.FName,
                    Description = Role2.DESCRIPTION
                },
                new ()
                {
                    Name = Role3.FName,
                    Description = Role3.DESCRIPTION
                },
                new ()
                {
                    Name = Role4.FName,
                    Description = Role4.DESCRIPTION
                },
                new ()
                {
                    Name = Role5.FName,
                    Description = Role5.DESCRIPTION
                },
            };
        }
    }
}