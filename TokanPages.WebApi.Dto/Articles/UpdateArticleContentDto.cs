using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Articles;

/// <summary>
/// Use it when you want to update existing content.
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateArticleContentDto
{
    /// <summary>
    /// Identification.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Title.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Description.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Text to upload.
    /// </summary>
    public string? TextToUpload { get; set; }

    /// <summary>
    /// Image name to upload.
    /// </summary>
    public string? ImageToUpload { get; set; }
}