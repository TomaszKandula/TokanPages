using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.Database.Initializer.Data.Roles;
using TokanPages.Persistence.Database.Initializer.Data.Users;

namespace TokanPages.Persistence.MigrationRunner.Data.UserRoles;

[ExcludeFromCodeCoverage]
public static class UserRole5
{
    public static readonly Guid Id = Guid.Parse("272566d2-6eb0-4f92-a716-6dcbfbe500ad");

    public static readonly Guid UserId = User4.Id;

    public static readonly Guid RoleId = Role2.Id;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-01-01 12:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}