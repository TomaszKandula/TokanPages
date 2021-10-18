namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class Permission4
    {
        public static readonly Guid Id = Guid.Parse("ad8f7d86-3bc1-44cf-9422-872adab1f7a3");

        public static string Name => nameof(Identity.Authorization.Permissions.CanPublishArticles);
    }
}