namespace TokanPages.Backend.Database.Initializer.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Domain.Entities;
    using Data.Roles;
    using Data.Users;

    [ExcludeFromCodeCoverage]
    public static class UserRolesSeeder
    {
        public static IEnumerable<UserRoles> SeedUserRoles()
        {
            return new List<UserRoles>
            {
                new ()
                {
                    Id = Guid.Parse("829c4857-7a80-42f9-97c9-62aff21127cf"),
                    UserId = User1.Id,
                    RoleId = Role1.Id
                },
                new ()
                {
                    Id = Guid.Parse("829c4857-7a80-42f9-97c9-62aff21127cf"),
                    UserId = User1.Id,
                    RoleId = Role2.Id
                },
                new ()
                {
                    Id = Guid.Parse("6227f1a3-3dd4-4800-a31b-be6ee1d388ef"),
                    UserId = User2.Id,
                    RoleId = Role2.Id
                },
                new ()
                {
                    Id = Guid.Parse("bc9ad7c2-0ea0-425b-a63f-cdc8582c521c"),
                    UserId = User3.Id,
                    RoleId = Role2.Id
                }
            };
        }
    }
}