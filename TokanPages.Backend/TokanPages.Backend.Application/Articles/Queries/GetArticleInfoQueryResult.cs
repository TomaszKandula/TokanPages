namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticleInfoQueryResult : GetArticlesQueryResult
{
    public int LikeCount { get; set; }

    public int UserLikes { get; set; }
}
