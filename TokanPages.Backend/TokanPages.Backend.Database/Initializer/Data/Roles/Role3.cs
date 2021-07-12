using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Roles
{
    [ExcludeFromCodeCoverage]
    public static class Role3
    {
        public static readonly Guid FId = Guid.Parse("ece95a5a-e6fd-414a-9a2a-62658c8bc11e");

        public static readonly string FName = nameof(Identity.Authorization.Roles.ArticlePublisher);
        
        public const string DESCRIPTION = "User can publish articles";
    }
}