namespace TokanPages.Persistence.Database.Initializer.Data.Permissions;

using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

[ExcludeFromCodeCoverage]
public static class Permission7
{
    public static readonly Guid Id = Guid.Parse("5cb9ae46-b588-4603-87ef-1e4878fec72c");

    public static string Name => nameof(Backend.Domain.Enums.Permissions.CanInsertComments);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}