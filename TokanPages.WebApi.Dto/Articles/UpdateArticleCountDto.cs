using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Articles;

/// <summary>
/// Use it when you want to update count.
/// </summary>
[ExcludeFromCodeCoverage]
public class UpdateArticleCountDto
{
    /// <summary>
    /// Identification.
    /// </summary>
    public Guid Id { get; set; }
}