using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class GetUserDetailsDto
{
    public required Guid UserId { get; init; }

    public required string EmailAddress { get; init; } = string.Empty;

    public required string UserAlias { get; init; } = string.Empty;

    public required string FirstName { get; init; } = string.Empty;

    public required string LastName { get; init; } = string.Empty;

    public required string UserAboutText { get; init; } = string.Empty; 

    public required string UserImageName  { get; init; } = string.Empty;

    public required string UserVideoName  { get; init; } = string.Empty;

    public required string CryptedPassword { get; init; }

    public Guid? ResetId { get; init; }

    public DateTime? ResetIdEnds { get; init; }

    public Guid? ActivationId { get; init; }

    public DateTime? ActivationIdEnds { get; init; }

    public required bool IsActivated { get; init; }

    public required bool IsVerified { get; init; }

    public required bool IsDeleted { get; init; }

    public required bool HasBusinessLock { get; init; }

    public required DateTime Registered { get; init; }

    public DateTime? Modified { get; init; }
}