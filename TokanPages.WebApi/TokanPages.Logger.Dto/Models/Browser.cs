using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Logger.Dto.Models;

/// <summary>
/// Browser details.
/// </summary>
[ExcludeFromCodeCoverage]
public class Browser
{
    /// <summary>
    /// Major version.
    /// </summary>
    public string? Major { get; set; }

    /// <summary>
    /// Browser name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Browser type.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Browser reported version.
    /// </summary>
    public string? Version { get; set; }
}