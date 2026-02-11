using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class GetUserPermissionDto
{
    public required Guid UserId { get; init; }

    public required Guid PermissionId {get; init;}

    public required string Name { get; init; } = string.Empty;
}