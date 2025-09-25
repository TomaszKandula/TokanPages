using TokanPages.Backend.Application.Articles.Models;
using TokanPages.Backend.Core.Paging;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetAllArticlesQueryResult : PagingResults<GetArticlesQueryResult>
{
    public List<ArticleCategoryDto>  ArticleCategories { get; set; } = new();
}
