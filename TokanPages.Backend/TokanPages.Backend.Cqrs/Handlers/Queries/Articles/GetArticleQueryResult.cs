namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles;

using System.Collections.Generic;
using Dto.Users;
using Dto.Content.Common;

public class GetArticleQueryResult : GetAllArticlesQueryResult
{
    public int LikeCount { get; set; }

    public int UserLikes { get; set; }

    public GetUserDto? Author { get; set; }

    public List<Section>? Text { get; set; }
}