using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class GetUserDetailsDto
{
    public Guid UserId { get; init; }

    public string EmailAddress { get; init; } = string.Empty;

    public string UserAlias { get; init; } = string.Empty;

    public string FirstName { get; init; } = string.Empty;

    public string LastName { get; init; } = string.Empty;

    public string UserAboutText { get; init; } = string.Empty; 

    public string UserImageName  { get; init; } = string.Empty;

    public required string CryptedPassword { get; init; }

    public required Guid? ResetId { get; init; }

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