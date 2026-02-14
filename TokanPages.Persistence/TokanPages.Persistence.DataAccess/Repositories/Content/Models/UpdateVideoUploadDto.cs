using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Persistence.DataAccess.Repositories.Content.Models;

[ExcludeFromCodeCoverage]
public class UpdateVideoUploadDto
{
    public required Guid TicketId { get; set; }

    public required VideoStatus Status { get; set; }

    public required bool IsSourceDeleted { get; set; }

    public required string? ProcessingWarning { get; set; }

    public required long InputSizeInBytes { get; set; }

    public required long OutputSizeInBytes { get; set; }
}