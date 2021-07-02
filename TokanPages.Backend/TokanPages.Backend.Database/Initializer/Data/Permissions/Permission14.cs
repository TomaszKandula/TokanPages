using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    [ExcludeFromCodeCoverage]
    public static class Permission14
    {
        public static readonly Guid FId = Guid.Parse("205169b5-55b4-4ea0-b9cf-6a5d1ea93748");

        public static string Name => Identity.Authorization.Permissions.CanSelectPhotoAlbums;
    }
}