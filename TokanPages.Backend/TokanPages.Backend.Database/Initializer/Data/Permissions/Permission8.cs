namespace TokanPages.Backend.Database.Initializer.Data.Permissions;

using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

[ExcludeFromCodeCoverage]
public static class Permission8
{
    public static readonly Guid Id = Guid.Parse("f5f3aa92-1f8f-4f77-9d6c-616f7c4e6a9f");

    public static string Name => nameof(Permissions.CanUpdateComments);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}