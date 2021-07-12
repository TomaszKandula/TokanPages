using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Roles
{
    [ExcludeFromCodeCoverage]
    public static class Role2
    {
        public static readonly Guid FId = Guid.Parse("73e95f02-d076-49d7-a68c-536a2c6ea02c");

        public static readonly string FName = nameof(Identity.Authorization.Roles.EverydayUser);
        
        public const string DESCRIPTION = "User";
    }
}