namespace TokanPages.WebApi.Services.Caching.Articles;

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;

public interface IArticlesCache
{
    Task<List<GetAllArticlesQueryResult>> GetArticles(bool isPublished = true, bool noCache = false);

    Task<GetArticleQueryResult> GetArticle(Guid id, bool noCache = false);
}