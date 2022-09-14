using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Articles;

/// <summary>
/// Use it when you want to change visibility
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateArticleVisibilityDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    public bool IsPublished { get; set; }
}