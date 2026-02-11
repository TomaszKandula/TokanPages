using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.User.Models;

[ExcludeFromCodeCoverage]
public class GetUserNoteDto
{
    public Guid Id { get; init; }

    public string Note { get; init; } = string.Empty;

    public Guid CreatedBy { get; init; }

    public DateTime CreatedAt { get; init; }

    public Guid? ModifiedBy { get; init; }

    public DateTime? ModifiedAt { get; init; }
}