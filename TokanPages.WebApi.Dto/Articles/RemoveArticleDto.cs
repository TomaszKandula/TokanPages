using System.Diagnostics.CodeAnalysis;

namespace TokanPages.WebApi.Dto.Articles;

/// <summary>
/// Use it when you want to remove existing article
/// </summary>
[ExcludeFromCodeCoverage]
public class RemoveArticleDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    public Guid Id { get; set; }
}