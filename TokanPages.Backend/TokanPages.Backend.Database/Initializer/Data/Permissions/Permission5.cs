using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    [ExcludeFromCodeCoverage]
    public static class Permission5
    {
        public static readonly Guid FId = Guid.Parse("a67048f6-84ef-4cb2-9477-6b99890935db");

        public static string Name => Identity.Authorization.Permissions.CanAddLikes;
    }
}