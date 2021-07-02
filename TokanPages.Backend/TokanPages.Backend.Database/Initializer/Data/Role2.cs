using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data
{
    [ExcludeFromCodeCoverage]
    public static class Role2
    {
        public static readonly string FName = Identity.Authorization.Roles.EverydayUser;
        public const string DESCRIPTION = "User";
    }
}