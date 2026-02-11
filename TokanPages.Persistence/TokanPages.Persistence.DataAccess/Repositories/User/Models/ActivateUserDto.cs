using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class ActivateUserDto
{
    public required bool IsActivated { get; init; }

    public required bool IsVerified { get; init; }

    public Guid? ActivationId { get; init; }

    public DateTime? ActivationIdEnds { get; init; }

    public required DateTime ModifiedAt { get; init; }
}