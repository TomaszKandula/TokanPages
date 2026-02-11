using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class CreateUserDto
{
    public required Guid UserId { get; init; }

    public required string UserAlias { get; init; }

    public required string EmailAddress { get; init; }

    public required string CryptedPassword { get; init; }

    public required Guid ActivationId { get; init; }

    public required DateTime ActivationIdEnds { get; init; }
}