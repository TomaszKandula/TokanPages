using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Content;

/// <summary>
/// Language item.
/// </summary>
[ExcludeFromCodeCoverage]
public class LanguageItem
{
    /// <summary>
    /// Identification.
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; } = "";
    
    /// <summary>
    /// Default flag.
    /// </summary>
    public bool IsDefault { get; set; }
}