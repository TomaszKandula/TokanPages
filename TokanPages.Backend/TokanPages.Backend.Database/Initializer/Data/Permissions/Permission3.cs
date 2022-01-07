namespace TokanPages.Backend.Database.Initializer.Data.Permissions;

using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

[ExcludeFromCodeCoverage]
public static class Permission3
{
    public static readonly Guid Id = Guid.Parse("80db7f7c-9ac1-446a-8a18-3ec3750b9929");

    public static string Name => nameof(Permissions.CanUpdateArticles);
}