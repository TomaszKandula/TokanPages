using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.MigrationRunner.Data.Roles;

[ExcludeFromCodeCoverage]
public static class Role4
{
    public static readonly Guid Id = Guid.Parse("03a8a216-91ab-4f9f-9d98-270c94e0f2bc");

    public const string Name = nameof(Backend.Domain.Enums.Roles.PhotoPublisher);

    public const string Description = "User can add albums and photos";

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-11-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}