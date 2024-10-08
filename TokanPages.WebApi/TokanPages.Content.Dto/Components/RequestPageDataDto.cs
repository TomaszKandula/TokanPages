using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Content.Dto.Components;

/// <summary>
/// Use it when you want to request page content.
/// </summary>
[ExcludeFromCodeCoverage]
public class RequestPageDataDto
{
    /// <summary>
    /// List of requested components.
    /// </summary>
    public List<string> Components { get; set; } = new();

    /// <summary>
    /// Page name using given components.
    /// </summary>
    public string? PageName { get; set; }
    
    /// <summary>
    /// Selected language.
    /// </summary>
    public string? Language { get; set; }
}