using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Persistence.DataAccess.Repositories.Content.Models;

[ExcludeFromCodeCoverage]
public class UpdateVideoUploadDto
{
    public required Guid TicketId { get; init; }

    public required VideoStatus Status { get; init; }

    public required bool IsSourceDeleted { get; init; }

    public required string? ProcessingWarning { get; init; }

    public required long InputSizeInBytes { get; init; }

    public required long OutputSizeInBytes { get; init; }
}