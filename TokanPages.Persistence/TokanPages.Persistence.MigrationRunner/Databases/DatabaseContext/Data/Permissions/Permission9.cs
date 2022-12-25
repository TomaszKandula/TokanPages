using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.MigrationRunner.Data.Permissions;

[ExcludeFromCodeCoverage]
public static class Permission9
{
    public static readonly Guid Id = Guid.Parse("3f8b9b60-9b6f-4841-89d5-94b553acae16");

    public static string Name => nameof(Backend.Domain.Enums.Permissions.CanPublishComments);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}