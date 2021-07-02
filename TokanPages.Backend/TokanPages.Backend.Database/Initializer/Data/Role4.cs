using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data
{
    [ExcludeFromCodeCoverage]
    public static class Role4
    {
        public static readonly string FName = Identity.Authorization.Roles.PhotoPublisher;
        public const string DESCRIPTION = "User can add albums and photos";
    }
}