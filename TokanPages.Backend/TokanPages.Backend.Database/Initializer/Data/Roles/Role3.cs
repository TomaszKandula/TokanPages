namespace TokanPages.Backend.Database.Initializer.Data.Roles
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class Role3
    {
        public static readonly Guid Id = Guid.Parse("ece95a5a-e6fd-414a-9a2a-62658c8bc11e");

        public const string Name = nameof(Identity.Authorization.Roles.ArticlePublisher);

        public const string Description = "User can publish articles";
    }
}