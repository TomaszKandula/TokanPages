using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class CreateUserPermissionDto
{
    public required Guid UserId { get; init; }

    public required Guid PermissionId { get; init; }
}