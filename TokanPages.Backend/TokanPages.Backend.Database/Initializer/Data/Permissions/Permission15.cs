using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    [ExcludeFromCodeCoverage]
    public static class Permission15
    {
        public static readonly Guid FId = Guid.Parse("72037cad-972b-42a5-bcdc-3f580435fd73");

        public static string Name => nameof(Identity.Authorization.Permissions.CanInsertPhotoAlbums);
    }
}