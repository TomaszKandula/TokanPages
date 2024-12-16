namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticleInfoQueryResult : GetArticlesQueryResult
{
    public int UserLikes { get; set; }
}
