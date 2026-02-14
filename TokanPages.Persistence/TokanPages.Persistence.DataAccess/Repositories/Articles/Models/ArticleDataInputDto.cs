using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

[ExcludeFromCodeCoverage]
public class ArticleDataInputDto
{
    public required Guid ArticleId { get; init; }

    public required string Title { get; init; } = string.Empty;

    public required string Description { get; init; } = string.Empty;

    public required string LanguageIso { get; init; } = string.Empty;
}