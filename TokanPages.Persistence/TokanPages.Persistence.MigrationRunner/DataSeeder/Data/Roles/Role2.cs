using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.MigrationRunner.DataSeeder.Data.Roles;

[ExcludeFromCodeCoverage]
public static class Role2
{
    public static readonly Guid Id = Guid.Parse("73e95f02-d076-49d7-a68c-536a2c6ea02c");

    public const string Name = nameof(Backend.Domain.Enums.Roles.EverydayUser);

    public const string Description = "User";

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-11-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}