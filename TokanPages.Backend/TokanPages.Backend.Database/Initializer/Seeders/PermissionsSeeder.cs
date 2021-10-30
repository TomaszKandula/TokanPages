namespace TokanPages.Backend.Database.Initializer.Seeders
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Domain.Entities;
    using Data.Permissions;

    [ExcludeFromCodeCoverage]
    public static class PermissionsSeeder
    {
        public static IEnumerable<Permissions> SeedPermissions()
        {
            return new List<Permissions>
            {
                new ()
                {
                    Id = Permission1.Id, 
                    Name = Permission1.Name
                },
                new () 
                { 
                    Id = Permission2.Id, 
                    Name = Permission2.Name 
                },
                new ()
                {
                    Id = Permission3.Id, 
                    Name = Permission3.Name
                },
                new ()
                {
                    Id = Permission4.Id, 
                    Name = Permission4.Name
                },
                new ()
                {
                    Id = Permission5.Id, 
                    Name = Permission5.Name
                },
                new ()
                {
                    Id = Permission6.Id, 
                    Name = Permission6.Name
                },
                new ()
                {
                    Id = Permission7.Id, 
                    Name = Permission7.Name
                },
                new ()
                {
                    Id = Permission8.Id, 
                    Name = Permission8.Name
                },
                new ()
                {
                    Id = Permission9.Id, 
                    Name = Permission9.Name
                },
                new ()
                {
                    Id = Permission10.Id, 
                    Name = Permission10.Name
                },
                new ()
                {
                    Id = Permission11.Id, 
                    Name = Permission11.Name
                },
                new ()
                {
                    Id = Permission12.Id, 
                    Name = Permission12.Name
                },
                new ()
                {
                    Id = Permission13.Id, 
                    Name = Permission13.Name
                },
                new ()
                {
                    Id = Permission14.Id, 
                    Name = Permission14.Name
                },
                new ()
                {
                    Id = Permission15.Id, 
                    Name = Permission15.Name
                },
                new ()
                {
                    Id = Permission16.Id, 
                    Name = Permission16.Name
                },
                new ()
                {
                    Id = Permission17.Id, 
                    Name = Permission17.Name
                }
            };
        }
    }
}