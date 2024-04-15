using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Backend.Application.Assets.Queries;

public class GetVideoStatusQueryResult
{
    public VideoStatus? Status { get; set; }

    public string? VideoUri { get; set; }

    public string? ThumbnailUri { get; set; }
}