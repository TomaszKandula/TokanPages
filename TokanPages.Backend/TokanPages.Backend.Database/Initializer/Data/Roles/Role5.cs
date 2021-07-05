using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Roles
{
    [ExcludeFromCodeCoverage]
    public static class Role5
    {
        public static readonly Guid FId = Guid.Parse("cd224afb-ac1f-4f5f-80f1-fdf432aaebe0");
        
        public static readonly string FName = Identity.Authorization.Roles.CommentPublisher;
        
        public const string DESCRIPTION = "User can add comments";
    }
}