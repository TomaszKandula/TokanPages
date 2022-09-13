using System.Collections.Generic;
using TokanPages.WebApi.Dto.Content.Common;
using TokanPages.WebApi.Dto.Users;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticleQueryResult : GetAllArticlesQueryResult
{
    public int LikeCount { get; set; }

    public int UserLikes { get; set; }

    public GetUserDto? Author { get; set; }

    public List<Section>? Text { get; set; }
}