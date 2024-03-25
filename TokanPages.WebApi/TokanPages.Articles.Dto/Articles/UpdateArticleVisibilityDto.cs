using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Articles.Dto.Articles;

/// <summary>
/// Use it when you want to change visibility.
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateArticleVisibilityDto
{
    /// <summary>
    /// Identification.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Published flag.
    /// </summary>
    public bool IsPublished { get; set; }
}