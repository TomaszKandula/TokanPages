namespace TokanPages.Persistence.Database.Initializer.Data.Permissions;

using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

[ExcludeFromCodeCoverage]
public static class Permission17
{
    public static readonly Guid Id = Guid.Parse("27a657cd-cc92-475f-804b-f5c7213d44e3");

    public static string Name => nameof(Backend.Domain.Enums.Permissions.CanPublishPhotoAlbums);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}