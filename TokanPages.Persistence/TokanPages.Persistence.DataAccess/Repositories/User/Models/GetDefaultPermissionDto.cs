using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class GetDefaultPermissionDto
{
    public required Guid Id { get; init; }

    public required Guid RoleId { get; init; }

    public required Guid PermissionId { get; init; }

    public required string RoleName { get; init; }

    public required string PermissionName { get; init; }
}