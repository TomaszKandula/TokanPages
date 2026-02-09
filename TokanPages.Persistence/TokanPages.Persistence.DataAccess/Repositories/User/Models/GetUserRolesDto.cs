using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class GetUserRolesDto
{
    public Guid UserId { get; init; }

    public Guid RoleId { get; init; }

    public string RoleName { get; init; } = string.Empty;

    public string Description { get; init; } =  string.Empty;
}