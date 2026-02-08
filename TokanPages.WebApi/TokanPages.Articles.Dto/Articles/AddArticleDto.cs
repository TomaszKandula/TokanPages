using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Articles.Dto.Articles;

/// <summary>
/// Use it when you want to add new article.
/// </summary>
[ExcludeFromCodeCoverage]
public class AddArticleDto
{
    /// <summary>
    /// Title.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Description.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Text to upload.
    /// </summary>
    public string TextToUpload { get; set; } = string.Empty;

    /// <summary>
    /// Image name to upload.
    /// </summary>
    public string ImageToUpload { get; set; } = string.Empty;

    /// <summary>
    /// Article language.
    /// </summary>
    public string LanguageIso { get; set; } = string.Empty;
}