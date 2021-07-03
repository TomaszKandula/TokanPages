using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    [ExcludeFromCodeCoverage]
    public static class Permission4
    {
        public static readonly Guid FId = Guid.Parse("ad8f7d86-3bc1-44cf-9422-872adab1f7a3");

        public static string Name => Identity.Authorization.Permissions.CanPublishArticles;
    }
}