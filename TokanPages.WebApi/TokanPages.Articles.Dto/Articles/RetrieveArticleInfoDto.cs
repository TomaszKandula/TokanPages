using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Articles.Dto.Articles;

/// <summary>
/// Use it when you want to retrieve article info.
/// </summary>
[ExcludeFromCodeCoverage]
public class RetrieveArticleInfoDto
{
    /// <summary>
    /// List of article IDs.
    /// </summary>
    public List<Guid> ArticleIds { get; set; } = new();

    /// <summary>
    /// Enable/disable REDIS cache.
    /// </summary>
    public bool NoCache { get; set; }
}