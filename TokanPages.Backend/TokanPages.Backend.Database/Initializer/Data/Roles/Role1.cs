using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Roles
{
    [ExcludeFromCodeCoverage]
    public static class Role1
    {
        public static readonly string FName = Identity.Authorization.Roles.GodOfAsgard;
        public const string DESCRIPTION = "Admin";
    }
}