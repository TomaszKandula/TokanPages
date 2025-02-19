using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Content.Components.Models;

/// <summary>
/// Language item.
/// </summary>
[ExcludeFromCodeCoverage]
public class LanguageItem
{
    /// <summary>
    /// Language ID.
    /// </summary>
    public string Id { get; set; } = "";

    /// <summary>
    /// Language ISO 639-1.
    /// </summary>
    public string Iso { get; set; } = "";

    /// <summary>
    /// Language name.
    /// </summary>
    public string Name { get; set; } = "";

    /// <summary>
    /// Default flag.
    /// </summary>
    public bool IsDefault { get; set; }
}