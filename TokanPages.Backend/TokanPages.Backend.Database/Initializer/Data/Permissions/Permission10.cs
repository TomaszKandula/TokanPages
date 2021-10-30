namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class Permission10
    {
        public static readonly Guid Id = Guid.Parse("1b1b7199-ad5b-4137-89f4-8ba194196e35");

        public static string Name => nameof(Identity.Authorization.Permissions.CanSelectPhotos);
    }
}