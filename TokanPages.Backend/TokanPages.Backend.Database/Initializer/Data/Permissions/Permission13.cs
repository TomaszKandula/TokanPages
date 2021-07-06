using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    [ExcludeFromCodeCoverage]
    public static class Permission13
    {
        public static readonly Guid FId = Guid.Parse("9555af70-4823-47e7-b702-3d09e6f7a83e");

        public static string Name => nameof(Identity.Authorization.Permissions.CanPublishPhotos);
    }
}