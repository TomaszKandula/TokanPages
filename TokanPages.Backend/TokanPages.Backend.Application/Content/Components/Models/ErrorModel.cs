using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Application.Content.Components.Models;

/// <summary>
/// General error text content.
/// </summary>
[ExcludeFromCodeCoverage]
public class ErrorModel
{
    /// <summary>
    /// Language ID.
    /// </summary>
    public string Language { get; set; } = "";

    /// <summary>
    /// Title.
    /// </summary>
    public string Title { get; set; } = "";

    /// <summary>
    /// Subtitle.
    /// </summary>
    public string Subtitle { get; set; } = "";

    /// <summary>
    /// Text.
    /// </summary>
    public string Text { get; set; } = "";

    /// <summary>
    /// URL value.
    /// </summary>
    public string LinkHref { get; set; } = "";

    /// <summary>
    /// Link short description.
    /// </summary>
    public string LinkText { get; set; } = "";

    /// <summary>
    /// Footer text.
    /// </summary>
    public string Footer { get; set; } = "";
}