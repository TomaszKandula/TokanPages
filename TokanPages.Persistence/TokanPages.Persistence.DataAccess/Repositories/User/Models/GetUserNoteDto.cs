using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class GetUserNoteDto
{
    public required Guid Id { get; init; }

    public required string Note { get; init; } = string.Empty;

    public required Guid CreatedBy { get; init; }

    public required DateTime CreatedAt { get; init; }

    public Guid? ModifiedBy { get; init; }

    public DateTime? ModifiedAt { get; init; }
}