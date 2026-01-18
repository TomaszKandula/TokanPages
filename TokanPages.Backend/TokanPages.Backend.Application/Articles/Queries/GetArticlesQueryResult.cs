using TokanPages.Backend.Core.Paging;
using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticlesQueryResult : PagingResults<ArticleDataDto>
{
    public List<ArticleCategoryDto>  ArticleCategories { get; set; } = new();
}
