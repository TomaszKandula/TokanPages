﻿using TokanPages.Backend.Application.Articles.Models;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticleQueryResult : GetArticlesQueryResult
{
    public int UserLikes { get; set; }

    public GetUserDto? Author { get; set; }

    public List<ArticleSectionDto>? Text { get; set; }
}