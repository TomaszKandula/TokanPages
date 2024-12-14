using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Content.Dto.Cached;

/// <summary>
/// Use it when you want to perform SPA snapshotting.
/// </summary>
[ExcludeFromCodeCoverage]
public class RequestProcessingDto
{
    /// <summary>
    /// SPA source.
    /// </summary>
    public string GetUrl { get; set; } = "";

    /// <summary>
    /// SPA upload endpoint URL.
    /// </summary>
    public string PostUrl { get; set; } = "";

    /// <summary>
    /// List of static files to be uploaded.
    /// </summary>
    public string[] Files { get; set; } = Array.Empty<string>();

    /// <summary>
    /// List of pages to be snapshot.
    /// </summary>
    public IEnumerable<RoutePathDto>? Paths { get; set; }
}