using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class UpdateSignupDetailsDto
{
    public required Guid UserId { get; init; }

    public required string CryptedPassword { get; init; }

    public required Guid ActivationId { get; init; }

    public required DateTime ActivationIdEnds { get; init; }
}