using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class ResetUserPasswordDto
{
    public required Guid UserId { get; init; }

    public required int ResetMaturity { get; init; }

    public required Guid ResetId { get; init; }
}