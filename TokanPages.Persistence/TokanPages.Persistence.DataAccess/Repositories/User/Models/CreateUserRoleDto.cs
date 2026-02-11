using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class CreateUserRoleDto
{
    public required Guid UserId { get; init; }

    public required Guid RoleId { get; init; }
}