using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.Database.Initializer.Data.Permissions;

[ExcludeFromCodeCoverage]
public static class Permission14
{
    public static readonly Guid Id = Guid.Parse("205169b5-55b4-4ea0-b9cf-6a5d1ea93748");

    public static string Name => nameof(Backend.Domain.Enums.Permissions.CanSelectPhotoAlbums);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}