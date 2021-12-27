namespace TokanPages.Backend.Database.Initializer.Data.Roles;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public static class Role2
{
    public static readonly Guid Id = Guid.Parse("73e95f02-d076-49d7-a68c-536a2c6ea02c");

    public const string Name = nameof(Identity.Authorization.Roles.EverydayUser);

    public const string Description = "User";
}