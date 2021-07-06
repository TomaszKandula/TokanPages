using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    [ExcludeFromCodeCoverage]
    public static class Permission7
    {
        public static readonly Guid FId = Guid.Parse("5cb9ae46-b588-4603-87ef-1e4878fec72c");

        public static string Name => nameof(Identity.Authorization.Permissions.CanInsertComments);
    }
}