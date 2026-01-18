using System.Linq.Expressions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticlesQueryHandler : TableRequestHandler<ArticleDataDto, GetArticlesQuery, GetArticlesQueryResult>
{
    private readonly IUserService _userService;

    private readonly IArticlesRepository _articlesRepository;

    public GetArticlesQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IUserService userService, 
        IArticlesRepository articlesRepository) 
        : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _articlesRepository = articlesRepository;
    }

    public override IDictionary<string, Expression<Func<ArticleDataDto, object>>> GetOrderingExpressions() => GetSortingConfig();

    public override async Task<GetArticlesQueryResult> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
    {
        var userLanguage = _userService.GetRequestUserLanguage();
        var foundArticleIds = await _articlesRepository.GetSearchResult(request.SearchTerm, cancellationToken);

        var sortingConfig = GetSortingConfig();
        var articles = await _articlesRepository.GetArticleList(request.IsPublished, request.SearchTerm, request.CategoryId, foundArticleIds, sortingConfig, cancellationToken);
        var categories = await _articlesRepository.GetArticleCategories(userLanguage, cancellationToken);

        return new GetArticlesQueryResult
        {
            PagingInfo = request,
            TotalSize = articles.Count,
            ArticleCategories = categories,
            Results = articles
        };
    }

    private static Dictionary<string, Expression<Func<ArticleDataDto, object>>> GetSortingConfig()
    {
        return new Dictionary<string, Expression<Func<ArticleDataDto, object>>>(StringComparer.OrdinalIgnoreCase)
        {
            {nameof(ArticleDataDto.Title), articlesQueryResult => articlesQueryResult.Title},
            {nameof(ArticleDataDto.Description), articlesQueryResult => articlesQueryResult.Description},
            {nameof(ArticleDataDto.CreatedAt), articlesQueryResult => articlesQueryResult.CreatedAt}
        };
    }
}