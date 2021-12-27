namespace TokanPages.Backend.Database.Initializer.Data.Roles;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public static class Role5
{
    public static readonly Guid Id = Guid.Parse("cd224afb-ac1f-4f5f-80f1-fdf432aaebe0");

    public const string Name = nameof(Identity.Authorization.Roles.CommentPublisher);

    public const string Description = "User can add comments";
}