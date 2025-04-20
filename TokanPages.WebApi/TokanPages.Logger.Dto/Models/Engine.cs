using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Logger.Dto.Models;

/// <summary>
/// Browser engine details. 
/// </summary>
[ExcludeFromCodeCoverage]
public class Engine
{
    /// <summary>
    /// Engine name.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Engine version.
    /// </summary>
    public string? Version { get; set; }
}