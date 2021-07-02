using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Database.Initializer.Data.Permissions;

namespace TokanPages.Backend.Database.Initializer.Seeders
{
    [ExcludeFromCodeCoverage]
    public class PermissionsSeeder
    {
        public static IEnumerable<Permissions> SeedPermissions()
        {
            return new List<Permissions>
            {
                new () { Name = Permission1.Name },
                new () { Name = Permission2.Name },
                new () { Name = Permission3.Name },
                new () { Name = Permission4.Name },
                new () { Name = Permission5.Name },
                new () { Name = Permission6.Name },
                new () { Name = Permission7.Name },
                new () { Name = Permission8.Name },
                new () { Name = Permission9.Name },
                new () { Name = Permission10.Name },
                new () { Name = Permission11.Name },
                new () { Name = Permission12.Name },
                new () { Name = Permission13.Name },
                new () { Name = Permission14.Name },
                new () { Name = Permission15.Name },
                new () { Name = Permission16.Name },
                new () { Name = Permission17.Name }
            };
        }
    }
}