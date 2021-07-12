using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    [ExcludeFromCodeCoverage]
    public static class Permission9
    {
        public static readonly Guid FId = Guid.Parse("3f8b9b60-9b6f-4841-89d5-94b553acae16");

        public static string Name => nameof(Identity.Authorization.Permissions.CanPublishComments);
    }
}