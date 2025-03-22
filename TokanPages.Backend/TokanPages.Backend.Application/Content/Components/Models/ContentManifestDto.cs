using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Content.Components.Models;

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
    /// Default key.
    /// </summary>
    public string Default { get; set; } = "";
    
    /// <summary>
    /// Registered languages.
    /// </summary>
    public List<LanguageModel> Languages { get; set; } = new();

    /// <summary>
    /// Error boundary text content in different languages.
    /// </summary>
    public List<ErrorModel> ErrorBoundary { get; set; } = new();
}