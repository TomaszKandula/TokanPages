using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class UpdateUserPasswordDto
{
    public required string CryptedPassword { get; init; }

    public required Guid? ResetId { get; init; }

    public required DateTime? ResetIdEnds { get; init; }

    public required DateTime ModifiedAt { get; init; }
}