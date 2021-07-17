namespace TokanPages.Backend.Database.Initializer.Data.Roles
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public static class Role1
    {
        public static readonly Guid FId = Guid.Parse("413f8fc8-7f25-40e0-88f3-9f846288f6c5");

        public static readonly string FName = nameof(Identity.Authorization.Roles.GodOfAsgard);
        
        public const string DESCRIPTION = "Admin";
    }
}