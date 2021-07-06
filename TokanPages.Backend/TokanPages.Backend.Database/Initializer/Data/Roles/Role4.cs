using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Roles
{
    [ExcludeFromCodeCoverage]
    public static class Role4
    {
        public static readonly Guid FId = Guid.Parse("03a8a216-91ab-4f9f-9d98-270c94e0f2bc");
        
        public static readonly string FName = nameof(Identity.Authorization.Roles.PhotoPublisher);
        
        public const string DESCRIPTION = "User can add albums and photos";
    }
}