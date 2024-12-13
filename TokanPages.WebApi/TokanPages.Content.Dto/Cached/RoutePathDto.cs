using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Content.Dto.Cached;

/// <summary>
/// Use it when you want to perform SPA snapshotting.
/// </summary>
[ExcludeFromCodeCoverage]
public class RoutePathDto
{
    /// <summary>
    /// URL address.
    /// </summary>
    public string Url { get; set; } = "";

    /// <summary>
    /// Page name.
    /// </summary>
    public string Name { get; set; } = "";
}