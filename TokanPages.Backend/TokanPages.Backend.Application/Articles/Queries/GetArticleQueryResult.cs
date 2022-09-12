namespace TokanPages.Backend.Application.Handlers.Queries.Articles;

using System.Collections.Generic;
using WebApi.Dto.Users;
using WebApi.Dto.Content.Common;

public class GetArticleQueryResult : GetAllArticlesQueryResult
{
    public int LikeCount { get; set; }

    public int UserLikes { get; set; }

    public GetUserDto? Author { get; set; }

    public List<Section>? Text { get; set; }
}