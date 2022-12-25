using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.Database.Initializer.Data.Roles;
using TokanPages.Persistence.Database.Initializer.Data.Users;

namespace TokanPages.Persistence.MigrationRunner.Databases.DatabaseContext.Data.UserRoles;

[ExcludeFromCodeCoverage]
public static class UserRole1
{
    public static readonly Guid Id = Guid.Parse("829c4857-7a80-42f9-97c9-62aff21127cf");

    public static readonly Guid UserId = User1.Id;

    public static readonly Guid RoleId = Role1.Id;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-01-01 12:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}