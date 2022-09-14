using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.Database.Initializer.Data.Permissions;

[ExcludeFromCodeCoverage]
public static class Permission11
{
    public static readonly Guid Id = Guid.Parse("a91cf136-0bec-428a-805b-6517cfae3f42");

    public static string Name => nameof(Backend.Domain.Enums.Permissions.CanInsertPhotos);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}