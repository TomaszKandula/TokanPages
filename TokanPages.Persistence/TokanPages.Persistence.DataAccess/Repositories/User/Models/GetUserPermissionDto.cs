using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class GetUserPermissionDto
{
    public Guid UserId { get; init; }

    public Guid PermissionId {get; init;}

    public string Name { get; init; } = string.Empty;
}