using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.MigrationRunner.DataSeeder.Data.Permissions;

[ExcludeFromCodeCoverage]
public static class Permission4
{
    public static readonly Guid Id = Guid.Parse("ad8f7d86-3bc1-44cf-9422-872adab1f7a3");

    public static string Name => nameof(Backend.Domain.Enums.Permissions.CanPublishArticles);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}