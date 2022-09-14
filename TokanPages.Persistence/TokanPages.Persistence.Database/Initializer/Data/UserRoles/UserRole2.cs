using System.Diagnostics.CodeAnalysis;
using TokanPages.Persistence.Database.Initializer.Data.Roles;
using TokanPages.Persistence.Database.Initializer.Data.Users;

namespace TokanPages.Persistence.Database.Initializer.Data.UserRoles;

[ExcludeFromCodeCoverage]
public static class UserRole2
{
    public static readonly Guid Id = Guid.Parse("5ba6f15b-5a63-4968-8eeb-18da32cb0877");

    public static readonly Guid UserId = User1.Id;

    public static readonly Guid RoleId = Role2.Id;

    public static readonly DateTime CreatedAt = DateTime.Parse("2020-01-01 12:11:01");

    public static readonly Guid CreatedBy = Guid.Empty;

    public static readonly DateTime? ModifiedAt = null;

    public static readonly Guid? ModifiedBy = null;
}