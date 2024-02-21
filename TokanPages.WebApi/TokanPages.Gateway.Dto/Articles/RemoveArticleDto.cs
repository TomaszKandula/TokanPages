using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Gateway.Dto.Articles;

/// <summary>
/// Use it when you want to remove existing article.
/// </summary>
[ExcludeFromCodeCoverage]
public class RemoveArticleDto
{
    /// <summary>
    /// Identification.
    /// </summary>
    public Guid Id { get; set; }
}