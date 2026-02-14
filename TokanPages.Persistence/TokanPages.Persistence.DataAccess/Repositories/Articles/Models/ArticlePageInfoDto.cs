using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

[ExcludeFromCodeCoverage]
public class ArticlePageInfoDto
{
    public int PageNumber { get; init; }

    public int PageSize { get; init; }

    public string OrderByColumn { get; init; } = string.Empty;

    public string OrderByAscending { get; init; } = "ASC";
}