using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Database.Initializer.Data.Roles;
using TokanPages.Backend.Database.Initializer.Data.Permissions;

namespace TokanPages.Backend.Database.Initializer.Seeders
{
    [ExcludeFromCodeCoverage]
    public static class DefaultPermissionsSeeder
    {
        public static IEnumerable<DefaultPermissions> SeedDefaultPermissions()
        {
            return new List<DefaultPermissions>
            {
                // GodOfAsgard role has all the permissions
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission1.FId
                },
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission2.FId
                },
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission3.FId
                },
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission4.FId
                },
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission5.FId
                },
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission6.FId
                },
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission7.FId
                },
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission8.FId
                },
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission9.FId
                },
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission10.FId
                },
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission11.FId
                },
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission12.FId
                },
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission13.FId
                },
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission14.FId
                },
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission15.FId
                },
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission16.FId
                },
                new ()
                {
                    RoleId = Role1.FId,
                    PermissionId = Permission17.FId
                },
                // EverydayUser has few permissions 
                new ()
                {
                    RoleId = Role2.FId,
                    PermissionId = Permission1.FId
                },
                new ()
                {
                    RoleId = Role2.FId,
                    PermissionId = Permission2.FId
                },
                new ()
                {
                    RoleId = Role2.FId,
                    PermissionId = Permission3.FId
                },
                new ()
                {
                    RoleId = Role2.FId,
                    PermissionId = Permission5.FId
                },
                new ()
                {
                    RoleId = Role2.FId,
                    PermissionId = Permission6.FId
                },
                new ()
                {
                    RoleId = Role2.FId,
                    PermissionId = Permission7.FId
                },
                new ()
                {
                    RoleId = Role2.FId,
                    PermissionId = Permission8.FId
                },
                new ()
                {
                    RoleId = Role2.FId,
                    PermissionId = Permission10.FId
                },
                new ()
                {
                    RoleId = Role2.FId,
                    PermissionId = Permission14.FId
                },
                // ArticlePublisher has one permission
                new ()
                {
                    RoleId = Role3.FId,
                    PermissionId = Permission4.FId
                },
                // PhotoPublisher has few permissions
                new ()
                {
                    RoleId = Role4.FId,
                    PermissionId = Permission11.FId
                },
                new ()
                {
                    RoleId = Role4.FId,
                    PermissionId = Permission12.FId
                },
                new ()
                {
                    RoleId = Role4.FId,
                    PermissionId = Permission13.FId
                },
                new ()
                {
                    RoleId = Role4.FId,
                    PermissionId = Permission15.FId
                },
                new ()
                {
                    RoleId = Role4.FId,
                    PermissionId = Permission16.FId
                },
                new ()
                {
                    RoleId = Role4.FId,
                    PermissionId = Permission17.FId
                },
                // CommentPublisher has one permission
                new ()
                {
                    RoleId = Role5.FId,
                    PermissionId = Permission9.FId
                }
            };
        }
    }
}