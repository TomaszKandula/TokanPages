using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    [ExcludeFromCodeCoverage]
    public static class Permission11
    {
        public static readonly Guid FId = Guid.Parse("a91cf136-0bec-428a-805b-6517cfae3f42");

        public static string Name => Identity.Authorization.Permissions.CanInsertPhotos;
    }
}