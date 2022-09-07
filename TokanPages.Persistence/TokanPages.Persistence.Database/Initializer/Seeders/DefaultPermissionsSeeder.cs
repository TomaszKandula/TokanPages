using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Persistence.Database.Initializer.Data.DefaultPermission;
using TokanPages.Persistence.Database.Initializer.Data.Permissions;
using TokanPages.Persistence.Database.Initializer.Data.Roles;

namespace TokanPages.Persistence.Database.Initializer.Seeders;

[ExcludeFromCodeCoverage]
public static class DefaultPermissionsSeeder
{
    public static IEnumerable<DefaultPermissions> SeedDefaultPermissions()
    {
        return new List<DefaultPermissions>
        {
            // GodOfAsgard role has all the permissions
            new()
            {
                Id = Guid.Parse("7053ed80-3e37-4e62-ae08-cd6f9d11c46d"),
                RoleId = Role1.Id,
                PermissionId = Permission1.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("70280943-c634-4be6-af23-143365d3c626"),
                RoleId = Role1.Id,
                PermissionId = Permission2.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("1cca9ecd-fe9a-4cf7-858e-df0c2678b679"),
                RoleId = Role1.Id,
                PermissionId = Permission3.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("731e1ff9-4c1b-4124-a8d3-eb8b3428074a"),
                RoleId = Role1.Id,
                PermissionId = Permission4.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("abf8f1a8-2ae9-43f9-8109-4c7301c0b2a9"),
                RoleId = Role1.Id,
                PermissionId = Permission5.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("5153f270-2142-4cd1-9e26-f81a37a0b26b"),
                RoleId = Role1.Id,
                PermissionId = Permission6.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("dd75da39-980c-4f11-9db3-4843f283e27e"),
                RoleId = Role1.Id,
                PermissionId = Permission7.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("d4b04ff4-76d0-4c08-b7a7-2609ed4fbce1"),
                RoleId = Role1.Id,
                PermissionId = Permission8.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("d1dd1995-e970-4816-b888-74b969b87eea"),
                RoleId = Role1.Id,
                PermissionId = Permission9.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("b0e2be7d-741c-4838-b8a1-3d0b00b00dca"),
                RoleId = Role1.Id,
                PermissionId = Permission10.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("9bb12ec8-ffda-411c-93b7-4ca165f767b4"),
                RoleId = Role1.Id,
                PermissionId = Permission11.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("99efb73b-01d6-47e1-95c3-24b7349af061"),
                RoleId = Role1.Id,
                PermissionId = Permission12.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("5b2abdbe-9b0e-456e-a666-3c48ea0734c7"),
                RoleId = Role1.Id,
                PermissionId = Permission13.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("af6b9b97-13c3-4e68-bac6-a09cdd324e11"),
                RoleId = Role1.Id,
                PermissionId = Permission14.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("3b316e3b-a93e-4228-848d-2fa9ef201de9"),
                RoleId = Role1.Id,
                PermissionId = Permission15.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("54d02e77-1187-403c-8f1e-a4c49539c4f7"),
                RoleId = Role1.Id,
                PermissionId = Permission16.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("2d54efb0-3cd5-4ad0-8f4d-621163279be4"),
                RoleId = Role1.Id,
                PermissionId = Permission17.Id,
                CreatedAt = DefaultPermission1.CreatedAt,
                CreatedBy = DefaultPermission1.CreatedBy,
                ModifiedAt = DefaultPermission1.ModifiedAt,
                ModifiedBy = DefaultPermission1.ModifiedBy
            },
            // EverydayUser has few permissions 
            new()
            {
                Id = Guid.Parse("6fdde491-ef5c-4279-83ae-a005facb8879"),
                RoleId = Role2.Id,
                PermissionId = Permission1.Id,
                CreatedAt = DefaultPermission2.CreatedAt,
                CreatedBy = DefaultPermission2.CreatedBy,
                ModifiedAt = DefaultPermission2.ModifiedAt,
                ModifiedBy = DefaultPermission2.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("6248c332-dc25-45a8-9685-84dcadefc6e4"),
                RoleId = Role2.Id,
                PermissionId = Permission2.Id,
                CreatedAt = DefaultPermission2.CreatedAt,
                CreatedBy = DefaultPermission2.CreatedBy,
                ModifiedAt = DefaultPermission2.ModifiedAt,
                ModifiedBy = DefaultPermission2.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("d1985796-ff6e-4bb8-bd00-fce6648ad6b0"),
                RoleId = Role2.Id,
                PermissionId = Permission3.Id,
                CreatedAt = DefaultPermission2.CreatedAt,
                CreatedBy = DefaultPermission2.CreatedBy,
                ModifiedAt = DefaultPermission2.ModifiedAt,
                ModifiedBy = DefaultPermission2.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("3369fb81-75d6-4e16-b1c8-51cb5db3032c"),
                RoleId = Role2.Id,
                PermissionId = Permission5.Id,
                CreatedAt = DefaultPermission2.CreatedAt,
                CreatedBy = DefaultPermission2.CreatedBy,
                ModifiedAt = DefaultPermission2.ModifiedAt,
                ModifiedBy = DefaultPermission2.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("063e6f0d-87ee-49f3-9309-9cf8b311a0c6"),
                RoleId = Role2.Id,
                PermissionId = Permission6.Id,
                CreatedAt = DefaultPermission2.CreatedAt,
                CreatedBy = DefaultPermission2.CreatedBy,
                ModifiedAt = DefaultPermission2.ModifiedAt,
                ModifiedBy = DefaultPermission2.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("370fe966-87f3-49f3-a69d-051cbf93305a"),
                RoleId = Role2.Id,
                PermissionId = Permission7.Id,
                CreatedAt = DefaultPermission2.CreatedAt,
                CreatedBy = DefaultPermission2.CreatedBy,
                ModifiedAt = DefaultPermission2.ModifiedAt,
                ModifiedBy = DefaultPermission2.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("6d0b64f2-c21d-48f0-8525-4863ef9cb3c8"),
                RoleId = Role2.Id,
                PermissionId = Permission8.Id,
                CreatedAt = DefaultPermission2.CreatedAt,
                CreatedBy = DefaultPermission2.CreatedBy,
                ModifiedAt = DefaultPermission2.ModifiedAt,
                ModifiedBy = DefaultPermission2.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("241812d3-ecbb-4242-a4f7-e143fa480e49"),
                RoleId = Role2.Id,
                PermissionId = Permission10.Id,
                CreatedAt = DefaultPermission2.CreatedAt,
                CreatedBy = DefaultPermission2.CreatedBy,
                ModifiedAt = DefaultPermission2.ModifiedAt,
                ModifiedBy = DefaultPermission2.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("2e337619-6b74-4e2a-a611-a691ff9a71c0"),
                RoleId = Role2.Id,
                PermissionId = Permission14.Id,
                CreatedAt = DefaultPermission2.CreatedAt,
                CreatedBy = DefaultPermission2.CreatedBy,
                ModifiedAt = DefaultPermission2.ModifiedAt,
                ModifiedBy = DefaultPermission2.ModifiedBy
            },
            // ArticlePublisher has one permission
            new()
            {
                Id = Guid.Parse("42c799e6-8ef5-4e11-90c0-d972d04bb7fa"),
                RoleId = Role3.Id,
                PermissionId = Permission4.Id,
                CreatedAt = DefaultPermission3.CreatedAt,
                CreatedBy = DefaultPermission3.CreatedBy,
                ModifiedAt = DefaultPermission3.ModifiedAt,
                ModifiedBy = DefaultPermission3.ModifiedBy
            },
            // PhotoPublisher has few permissions
            new()
            {
                Id = Guid.Parse("60047404-9ba0-4703-aff2-b653a2c03578"),
                RoleId = Role4.Id,
                PermissionId = Permission11.Id,
                CreatedAt = DefaultPermission4.CreatedAt,
                CreatedBy = DefaultPermission4.CreatedBy,
                ModifiedAt = DefaultPermission4.ModifiedAt,
                ModifiedBy = DefaultPermission4.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("634402c4-c13c-4872-a948-e0a307ad991a"),
                RoleId = Role4.Id,
                PermissionId = Permission12.Id,
                CreatedAt = DefaultPermission4.CreatedAt,
                CreatedBy = DefaultPermission4.CreatedBy,
                ModifiedAt = DefaultPermission4.ModifiedAt,
                ModifiedBy = DefaultPermission4.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("af143f2e-12e2-47b3-976e-9b5570b35704"),
                RoleId = Role4.Id,
                PermissionId = Permission13.Id,
                CreatedAt = DefaultPermission4.CreatedAt,
                CreatedBy = DefaultPermission4.CreatedBy,
                ModifiedAt = DefaultPermission4.ModifiedAt,
                ModifiedBy = DefaultPermission4.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("b5f36e97-2bfa-4afd-b16e-1958ed9c6b72"),
                RoleId = Role4.Id,
                PermissionId = Permission15.Id,
                CreatedAt = DefaultPermission4.CreatedAt,
                CreatedBy = DefaultPermission4.CreatedBy,
                ModifiedAt = DefaultPermission4.ModifiedAt,
                ModifiedBy = DefaultPermission4.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("e3e4e422-1eb0-456e-9f79-b90ebe700e06"),
                RoleId = Role4.Id,
                PermissionId = Permission16.Id,
                CreatedAt = DefaultPermission4.CreatedAt,
                CreatedBy = DefaultPermission4.CreatedBy,
                ModifiedAt = DefaultPermission4.ModifiedAt,
                ModifiedBy = DefaultPermission4.ModifiedBy
            },
            new()
            {
                Id = Guid.Parse("57328588-fc2f-4437-9baf-95c387f42ebe"),
                RoleId = Role4.Id,
                PermissionId = Permission17.Id,
                CreatedAt = DefaultPermission4.CreatedAt,
                CreatedBy = DefaultPermission4.CreatedBy,
                ModifiedAt = DefaultPermission4.ModifiedAt,
                ModifiedBy = DefaultPermission4.ModifiedBy
            },
            // CommentPublisher has one permission
            new()
            {
                Id = Guid.Parse("c2115a37-39e7-4ad7-8854-8fe48329ea54"),
                RoleId = Role5.Id,
                PermissionId = Permission9.Id,
                CreatedAt = DefaultPermission5.CreatedAt,
                CreatedBy = DefaultPermission5.CreatedBy,
                ModifiedAt = DefaultPermission5.ModifiedAt,
                ModifiedBy = DefaultPermission5.ModifiedBy
            }
        };
    }
}