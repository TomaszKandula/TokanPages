using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.Roles;
using TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.Users;

namespace TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.UserRoles;

[ExcludeFromCodeCoverage]
public static class UserRole3
{
    public static readonly Guid Id = Guid.Parse("6227f1a3-3dd4-4800-a31b-be6ee1d388ef");

    public static readonly Guid UserId = User2.Id;

    public static readonly Guid RoleId = Role2.Id;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-01-01 12:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}