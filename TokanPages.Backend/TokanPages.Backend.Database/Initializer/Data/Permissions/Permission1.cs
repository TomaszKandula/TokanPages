using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    [ExcludeFromCodeCoverage]
    public static class Permission1
    {
        public static string Name => Identity.Authorization.Permissions.CanSelectArticles;
    }
}