using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data
{
    [ExcludeFromCodeCoverage]
    public static class Role5
    {
        public static readonly string FName = Identity.Authorization.Roles.CommentPublisher;
        public const string DESCRIPTION = "User can add comments";
    }
}