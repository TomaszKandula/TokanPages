namespace TokanPages.Persistence.Database.Initializer.Data.Roles;

using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

[ExcludeFromCodeCoverage]
public static class Role5
{
    public static readonly Guid Id = Guid.Parse("cd224afb-ac1f-4f5f-80f1-fdf432aaebe0");

    public const string Name = nameof(Backend.Domain.Enums.Roles.CommentPublisher);

    public const string Description = "User can add comments";

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-11-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}