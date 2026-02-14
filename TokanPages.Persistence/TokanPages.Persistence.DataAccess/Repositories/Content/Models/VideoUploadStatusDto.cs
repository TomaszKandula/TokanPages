using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Persistence.DataAccess.Repositories.Content.Models;

[ExcludeFromCodeCoverage]
public class VideoUploadStatusDto
{
    public required VideoStatus? Status { get; init; }

    public required string? VideoUri { get; init; }

    public required string? ThumbnailUri { get; init; }

    public required string SourceBlobUri { get; init; }

    public required string TargetVideoUri { get; set; }

    public required string TargetThumbnailUri { get; set; }
}