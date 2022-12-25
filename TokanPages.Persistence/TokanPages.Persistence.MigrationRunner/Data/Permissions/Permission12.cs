using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.MigrationRunner.DataSeeder.Data.Permissions;

[ExcludeFromCodeCoverage]
public static class Permission12
{
    public static readonly Guid Id = Guid.Parse("d1b45530-ede9-4b20-8b8f-11e18243463e");

    public static string Name => nameof(Backend.Domain.Enums.Permissions.CanUpdatePhotos);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}