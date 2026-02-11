using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class CreateUserPermissionDto
{
    public required Guid UserId { get; set; }

    public required Guid PermissionId { get; set; }
}