using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Content.Models;

/// <summary>
/// Content manifest definition.
/// </summary>
[ExcludeFromCodeCoverage]
public class ContentManifestDto
{
    /// <summary>
    /// Manifest version.
    /// </summary>
    public string Version { get; set; } = "";
    
    /// <summary>
    /// Create date and time.
    /// </summary>
    public DateTime Created { get; set; }
    
    /// <summary>
    /// Update date and time.
    /// </summary>
    public DateTime Updated { get; set; }

    /// <summary>
    /// Registered languages.
    /// </summary>
    public List<LanguageItem> Languages { get; set; } = new();
}