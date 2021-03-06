using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    [ExcludeFromCodeCoverage]
    public static class Permission17
    {
        public static readonly Guid FId = Guid.Parse("27a657cd-cc92-475f-804b-f5c7213d44e3");

        public static string Name => Identity.Authorization.Permissions.CanPublishPhotoAlbums;
    }
}