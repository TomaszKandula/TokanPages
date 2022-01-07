namespace TokanPages.Backend.Database.Initializer.Data.Permissions;

using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

[ExcludeFromCodeCoverage]
public static class Permission5
{
    public static readonly Guid Id = Guid.Parse("a67048f6-84ef-4cb2-9477-6b99890935db");

    public static string Name => nameof(Permissions.CanAddLikes);
}