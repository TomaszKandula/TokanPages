using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class UpdateUserDto
{
    public required Guid UserId { get; init; }

    public required string UserAlias { get; init; }

    public required string EmailAddress { get; init; }

    public required bool IsActivated { get; init; }

    public required bool IsVerified { get; init; }
}