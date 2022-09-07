namespace TokanPages.Persistence.Database.Initializer.Data.Permissions;

using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

[ExcludeFromCodeCoverage]
public static class Permission16
{
    public static readonly Guid Id = Guid.Parse("2f159e38-674d-40c2-9407-41d4a4752954");

    public static string Name => nameof(Backend.Domain.Enums.Permissions.CanUpdatePhotoAlbums);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}