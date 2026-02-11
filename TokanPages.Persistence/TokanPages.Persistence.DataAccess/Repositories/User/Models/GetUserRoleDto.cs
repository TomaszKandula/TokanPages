using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class GetUserRoleDto
{
    public required Guid UserId { get; init; }

    public required Guid RoleId { get; init; }

    public required string RoleName { get; init; } = string.Empty;

    public required string Description { get; init; } =  string.Empty;
}