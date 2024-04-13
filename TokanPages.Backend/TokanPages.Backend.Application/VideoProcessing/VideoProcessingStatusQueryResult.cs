using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Application.VideoProcessing;

public class VideoProcessingStatusQueryResult
{
    public VideoStatus? Status { get; set; }

    public string? VideoUri { get; set; }

    public string? ThumbnailUri { get; set; }
}