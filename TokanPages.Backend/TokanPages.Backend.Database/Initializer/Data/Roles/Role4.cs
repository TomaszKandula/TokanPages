namespace TokanPages.Backend.Database.Initializer.Data.Roles
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class Role4
    {
        public static readonly Guid Id = Guid.Parse("03a8a216-91ab-4f9f-9d98-270c94e0f2bc");

        public const string Name = nameof(Identity.Authorization.Roles.PhotoPublisher);

        public const string Description = "User can add albums and photos";
    }
}