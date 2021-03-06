using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    [ExcludeFromCodeCoverage]
    public static class Permission16
    {
        public static readonly Guid FId = Guid.Parse("2f159e38-674d-40c2-9407-41d4a4752954");

        public static string Name => Identity.Authorization.Permissions.CanUpdatePhotoAlbums;
    }
}