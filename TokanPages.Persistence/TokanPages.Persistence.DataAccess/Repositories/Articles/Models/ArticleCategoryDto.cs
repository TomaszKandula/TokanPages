using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

/// <summary>
/// Gets list of article category IDs.
/// </summary>
[ExcludeFromCodeCoverage]
public class ArticleCategoryDto
{
    public required Guid Id { get; init; }

    public required string CategoryName { get; init; } = string.Empty;
}