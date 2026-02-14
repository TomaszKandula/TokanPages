using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

[ExcludeFromCodeCoverage]
public class ArticlePageInfoDto
{
    public required int PageNumber { get; init; }

    public required int PageSize { get; init; }

    public required string OrderByColumn { get; init; } = string.Empty;

    public required string OrderByAscending { get; init; } = "ASC";
}