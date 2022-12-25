using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.MigrationRunner.DataSeeder.Data.Permissions;

[ExcludeFromCodeCoverage]
public static class Permission1
{
    public static readonly Guid Id = Guid.Parse("7e041e7f-c7bb-486b-9b09-a931015c36fd");

    public static string Name => nameof(Backend.Domain.Enums.Permissions.CanSelectArticles);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}