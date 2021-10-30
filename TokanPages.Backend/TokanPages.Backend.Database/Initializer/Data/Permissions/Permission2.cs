namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class Permission2
    {
        public static readonly Guid Id = Guid.Parse("070483c4-98cb-4b2a-be47-6c85c96854ba");

        public static string Name => nameof(Identity.Authorization.Permissions.CanInsertArticles);
    }
}