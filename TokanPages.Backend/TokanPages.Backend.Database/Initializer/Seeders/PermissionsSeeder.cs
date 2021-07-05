using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Database.Initializer.Data.Permissions;

namespace TokanPages.Backend.Database.Initializer.Seeders
{
    [ExcludeFromCodeCoverage]
    public static class PermissionsSeeder
    {
        public static IEnumerable<Permissions> SeedPermissions()
        {
            return new List<Permissions>
            {
                new ()
                {
                    Id = Permission1.FId, 
                    Name = Permission1.Name
                },
                new () 
                { 
                    Id = Permission2.FId, 
                    Name = Permission2.Name 
                },
                new ()
                {
                    Id = Permission3.FId, 
                    Name = Permission3.Name
                },
                new ()
                {
                    Id = Permission4.FId, 
                    Name = Permission4.Name
                },
                new ()
                {
                    Id = Permission5.FId, 
                    Name = Permission5.Name
                },
                new ()
                {
                    Id = Permission6.FId, 
                    Name = Permission6.Name
                },
                new ()
                {
                    Id = Permission7.FId, 
                    Name = Permission7.Name
                },
                new ()
                {
                    Id = Permission8.FId, 
                    Name = Permission8.Name
                },
                new ()
                {
                    Id = Permission9.FId, 
                    Name = Permission9.Name
                },
                new ()
                {
                    Id = Permission10.FId, 
                    Name = Permission10.Name
                },
                new ()
                {
                    Id = Permission11.FId, 
                    Name = Permission11.Name
                },
                new ()
                {
                    Id = Permission12.FId, 
                    Name = Permission12.Name
                },
                new ()
                {
                    Id = Permission13.FId, 
                    Name = Permission13.Name
                },
                new ()
                {
                    Id = Permission14.FId, 
                    Name = Permission14.Name
                },
                new ()
                {
                    Id = Permission15.FId, 
                    Name = Permission15.Name
                },
                new ()
                {
                    Id = Permission16.FId, 
                    Name = Permission16.Name
                },
                new ()
                {
                    Id = Permission17.FId, 
                    Name = Permission17.Name
                }
            };
        }
    }
}