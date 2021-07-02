using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    [ExcludeFromCodeCoverage]
    public static class Permission8
    {
        public static readonly Guid FId = Guid.Parse("f5f3aa92-1f8f-4f77-9d6c-616f7c4e6a9f");

        public static string Name => Identity.Authorization.Permissions.CanUpdateComments;
    }
}