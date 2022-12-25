using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.Database.Initializer.Data.Roles;
using TokanPages.Persistence.Database.Initializer.Data.Users;

namespace TokanPages.Persistence.MigrationRunner.Data.UserRoles;

[ExcludeFromCodeCoverage]
public static class UserRole4
{
    public static readonly Guid Id = Guid.Parse("bc9ad7c2-0ea0-425b-a63f-cdc8582c521c");

    public static readonly Guid UserId = User3.Id;

    public static readonly Guid RoleId = Role2.Id;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-01-01 12:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}