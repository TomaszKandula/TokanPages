using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.Database.Initializer.Data.Permissions;

[ExcludeFromCodeCoverage]
public static class Permission13
{
    public static readonly Guid Id = Guid.Parse("9555af70-4823-47e7-b702-3d09e6f7a83e");

    public static string Name => nameof(Backend.Domain.Enums.Permissions.CanPublishPhotos);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}