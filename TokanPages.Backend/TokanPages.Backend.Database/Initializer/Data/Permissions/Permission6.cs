using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    [ExcludeFromCodeCoverage]
    public static class Permission6
    {
        public static readonly Guid FId = Guid.Parse("301fa9f6-f104-41ac-b8cf-49623de01937");

        public static string Name => nameof(Identity.Authorization.Permissions.CanSelectComments);
    }
}