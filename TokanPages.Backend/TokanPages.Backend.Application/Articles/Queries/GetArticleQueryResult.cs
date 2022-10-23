using TokanPages.WebApi.Dto.Articles;
using TokanPages.WebApi.Dto.Users;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticleQueryResult : GetAllArticlesQueryResult
{
    public int LikeCount { get; set; }

    public int UserLikes { get; set; }

    public GetUserDto? Author { get; set; }

    public List<ArticleSectionDto>? Text { get; set; }
}