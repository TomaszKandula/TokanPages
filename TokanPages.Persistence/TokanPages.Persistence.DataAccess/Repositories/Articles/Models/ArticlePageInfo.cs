using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

[ExcludeFromCodeCoverage]
public class ArticlePageInfo
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }

    public string OrderByColumn { get; set; } = string.Empty;

    public string OrderByAscending { get; set; } = "ASC";
}