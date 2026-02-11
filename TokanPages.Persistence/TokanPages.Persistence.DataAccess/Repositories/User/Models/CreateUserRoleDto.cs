using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class CreateUserRoleDto
{
    public required Guid UserId { get; set; }

    public required Guid RoleId { get; set; }
}