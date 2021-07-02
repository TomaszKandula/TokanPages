using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Roles
{
    [ExcludeFromCodeCoverage]
    public static class Role3
    {
        public static readonly string FName = Identity.Authorization.Roles.ArticlePublisher;
        public const string DESCRIPTION = "User can publish articles";
    }
}