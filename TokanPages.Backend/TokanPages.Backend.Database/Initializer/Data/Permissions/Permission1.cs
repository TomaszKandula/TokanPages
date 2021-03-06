using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Database.Initializer.Data.Permissions
{
    [ExcludeFromCodeCoverage]
    public static class Permission1
    {
        public static readonly Guid FId = Guid.Parse("7e041e7f-c7bb-486b-9b09-a931015c36fd");
        
        public static string Name => Identity.Authorization.Permissions.CanSelectArticles;
    }
}