namespace TokanPages.Persistence.Database.Initializer.Data.Permissions;

using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

[ExcludeFromCodeCoverage]
public static class Permission15
{
    public static readonly Guid Id = Guid.Parse("72037cad-972b-42a5-bcdc-3f580435fd73");

    public static string Name => nameof(Backend.Domain.Enums.Permissions.CanInsertPhotoAlbums);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}