using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.Permissions;

[ExcludeFromCodeCoverage]
public static class Permission5
{
    public static readonly Guid Id = Guid.Parse("a67048f6-84ef-4cb2-9477-6b99890935db");

    public static string Name => nameof(Backend.Domain.Enums.Permissions.CanAddLikes);

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-09-01 21:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}