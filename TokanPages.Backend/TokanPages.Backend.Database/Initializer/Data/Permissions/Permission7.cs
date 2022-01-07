namespace TokanPages.Backend.Database.Initializer.Data.Permissions;

using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

[ExcludeFromCodeCoverage]
public static class Permission7
{
    public static readonly Guid Id = Guid.Parse("5cb9ae46-b588-4603-87ef-1e4878fec72c");

    public static string Name => nameof(Permissions.CanInsertComments);
}