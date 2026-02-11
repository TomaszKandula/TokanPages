using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class UpdateUserDto
{
    public bool IsActivated { get; init; }

    public bool IsVerified { get; init; }

    public Guid? ActivationId { get; init; }

    public DateTime? ActivationIdEnds { get; init; }
}