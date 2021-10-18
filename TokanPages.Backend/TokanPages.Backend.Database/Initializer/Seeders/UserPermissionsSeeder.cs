namespace TokanPages.Backend.Database.Initializer.Seeders
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using Data.Permissions;
    using Domain.Entities;
    using Data.Users;

    [ExcludeFromCodeCoverage]
    public static class UserPermissionsSeeder
    {
        public static IEnumerable<UserPermissions> SeedUserPermissions()
        {
            return new List<UserPermissions>
            {
                // User1
                new ()
                {
                    Id = Guid.Parse("1eec34dc-72f9-46e6-b66b-13a82a83f7b1"),
                    UserId = User1.Id,
                    PermissionId = Permission1.Id
                },
                new ()
                {
                    Id = Guid.Parse("e27cd238-bdff-49c2-9101-04c63b473dc1"),
                    UserId = User1.Id,
                    PermissionId = Permission2.Id
                },
                new ()
                {
                    Id = Guid.Parse("07cd2292-5d86-4fcd-9b47-bcfd05d030e0"),
                    UserId = User1.Id,
                    PermissionId = Permission3.Id
                },
                new ()
                {
                    Id = Guid.Parse("de83f570-fa4f-4d02-9b58-f454f78541e4"),
                    UserId = User1.Id,
                    PermissionId = Permission5.Id
                },
                new ()
                {
                    Id = Guid.Parse("7eb09d64-6dea-4582-96e1-7a6cebe4a186"),
                    UserId = User1.Id,
                    PermissionId = Permission6.Id
                },
                new ()
                {
                    Id = Guid.Parse("ade8ca4f-29f2-4813-8072-9e5a929c0c88"),
                    UserId = User1.Id,
                    PermissionId = Permission7.Id
                },
                new ()
                {
                    Id = Guid.Parse("374ac0d2-334b-4cd6-b9dd-b0430253408b"),
                    UserId = User1.Id,
                    PermissionId = Permission8.Id
                },
                new ()
                {
                    Id = Guid.Parse("cf5641c9-0dc7-4443-a6b3-86aee2744553"),
                    UserId = User1.Id,
                    PermissionId = Permission10.Id
                },
                new ()
                {
                    Id = Guid.Parse("9613bc86-ec9c-4f6a-8ddc-6d1574688696"),
                    UserId = User1.Id,
                    PermissionId = Permission14.Id
                },
                // User2
                new ()
                {
                    Id = Guid.Parse("fb672ed1-e1d3-4b16-8b56-04751a3bd453"),
                    UserId = User2.Id,
                    PermissionId = Permission1.Id
                },
                new ()
                {
                    Id = Guid.Parse("e700df7c-c728-44dd-b384-ac535470f974"),
                    UserId = User2.Id,
                    PermissionId = Permission2.Id
                },
                new ()
                {
                    Id = Guid.Parse("51f5c9f7-28de-4b66-81f1-d1172de736c1"),
                    UserId = User2.Id,
                    PermissionId = Permission3.Id
                },
                new ()
                {
                    Id = Guid.Parse("3ad6a37d-ba5d-43bd-883c-2e8d0665016a"),
                    UserId = User2.Id,
                    PermissionId = Permission5.Id
                },
                new ()
                {
                    Id = Guid.Parse("6655947b-fe6b-42b2-9359-414f13c36ea9"),
                    UserId = User2.Id,
                    PermissionId = Permission6.Id
                },
                new ()
                {
                    Id = Guid.Parse("a8670d47-d3fd-4257-a1c4-ecf703f89c7b"),
                    UserId = User2.Id,
                    PermissionId = Permission7.Id
                },
                new ()
                {
                    Id = Guid.Parse("b1b7fd46-f1f6-4ec0-8bc9-93ad15f551cd"),
                    UserId = User2.Id,
                    PermissionId = Permission8.Id
                },
                new ()
                {
                    Id = Guid.Parse("31b2ff32-5b46-48b7-b201-f9ee80b82220"),
                    UserId = User2.Id,
                    PermissionId = Permission10.Id
                },
                new ()
                {
                    Id = Guid.Parse("30f1a8f5-b9aa-4901-a716-f9cb977c39d1"),
                    UserId = User2.Id,
                    PermissionId = Permission14.Id
                },
                // User3
                new ()
                {
                    Id = Guid.Parse("4fff3655-c0cc-4484-967c-4b68fab9d389"),
                    UserId = User3.Id,
                    PermissionId = Permission1.Id
                },
                new ()
                {
                    Id = Guid.Parse("9920cf44-5d02-49f9-8b5a-6431e28c8e27"),
                    UserId = User3.Id,
                    PermissionId = Permission2.Id
                },
                new ()
                {
                    Id = Guid.Parse("6c1a22b7-48e0-4b1a-bfbd-93e5feb08eea"),
                    UserId = User3.Id,
                    PermissionId = Permission3.Id
                },
                new ()
                {
                    Id = Guid.Parse("00889233-4003-437c-b0ef-144ed7ba813f"),
                    UserId = User3.Id,
                    PermissionId = Permission5.Id
                },
                new ()
                {
                    Id = Guid.Parse("4d2ad760-acd5-4976-a8c5-c398809ceb53"),
                    UserId = User3.Id,
                    PermissionId = Permission6.Id
                },
                new ()
                {
                    Id = Guid.Parse("bc6c02d4-c892-44ef-9d03-30783b94c51c"),
                    UserId = User3.Id,
                    PermissionId = Permission7.Id
                },
                new ()
                {
                    Id = Guid.Parse("0afbfc97-00d2-4de8-a1f0-bda751a5235c"),
                    UserId = User3.Id,
                    PermissionId = Permission8.Id
                },
                new ()
                {
                    Id = Guid.Parse("f5985988-7ee4-4c3a-b2ad-4a2f4cf101c2"),
                    UserId = User3.Id,
                    PermissionId = Permission10.Id
                },
                new ()
                {
                    Id = Guid.Parse("31bd8834-ef87-4272-a20f-51e5642dc5fb"),
                    UserId = User3.Id,
                    PermissionId = Permission14.Id
                }
            };
        }
    }
}