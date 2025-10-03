using TokanPages.Backend.Application.Articles.Models;
using TokanPages.Backend.Core.Paging;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticlesQueryResult : PagingResults<ArticleDataDto>
{
    public List<ArticleCategoryDto>  ArticleCategories { get; set; } = new();
}
