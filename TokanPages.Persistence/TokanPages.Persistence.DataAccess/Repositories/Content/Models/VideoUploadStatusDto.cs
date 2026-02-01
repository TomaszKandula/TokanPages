using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Persistence.DataAccess.Repositories.Content.Models;

[ExcludeFromCodeCoverage]
public class VideoUploadStatusDto
{
    public VideoStatus? Status { get; set; }

    public string? VideoUri { get; set; }

    public string? ThumbnailUri { get; set; }
}