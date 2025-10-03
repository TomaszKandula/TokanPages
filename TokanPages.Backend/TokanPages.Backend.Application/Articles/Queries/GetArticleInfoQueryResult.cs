using TokanPages.Backend.Application.Articles.Models;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticleInfoQueryResult : ArticleDataDto
{
    public int UserLikes { get; set; }
}
