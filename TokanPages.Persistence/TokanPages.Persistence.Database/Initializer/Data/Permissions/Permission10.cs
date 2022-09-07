namespace TokanPages.Persistence.Database.Initializer.Data.Permissions;

using System;
using System.Diagnostics.CodeAnalysis;
using Domain.Enums;

[ExcludeFromCodeCoverage]
public static class Permission10
{
    public static readonly Guid Id = Guid.Parse("1b1b7199-ad5b-4137-89f4-8ba194196e35");

    public static string Name => nameof(Backend.Domain.Enums.Permissions.CanSelectPhotos);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}