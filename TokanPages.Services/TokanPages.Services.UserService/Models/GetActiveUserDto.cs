using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.UserService.Models;

[ExcludeFromCodeCoverage]
public class GetActiveUserDto
{
    public required Guid UserId { get; init; }

    public required string UserAlias { get; init; }

    public required string EmailAddress { get; init; }

    public required string CryptedPassword { get; init; }

    public required Guid? ResetId { get; init; }

    public required Guid CreatedBy { get; init; }

    public required DateTime CreatedAt { get; init; }

    public required bool IsActivated { get; init; }

    public required bool IsVerified { get; init; }

    public required bool IsDeleted { get; init; }

    public required bool HasBusinessLock { get; init; }
}