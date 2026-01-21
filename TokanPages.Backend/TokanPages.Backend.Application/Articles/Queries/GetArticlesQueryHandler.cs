using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticlesQueryHandler : RequestHandler<GetArticlesQuery, GetArticlesQueryResult>
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

    public override async Task<GetArticlesQueryResult> Handle(GetArticlesQuery request, CancellationToken cancellationToken)
    {
        var userLanguage = _userService.GetRequestUserLanguage();
        var filterById = await _articlesRepository.GetSearchResult(request.SearchTerm);
        var pageInfo = new ArticlePageInfoDto
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            OrderByColumn = request.OrderByColumn,
            OrderByAscending = request.OrderByAscending ? "ASC" : "DESC"
        };

        var articles = await _articlesRepository.GetArticleList(request.IsPublished, request.SearchTerm, request.CategoryId, filterById, pageInfo);
        var categories = await _articlesRepository.GetArticleCategories(userLanguage);

        return new GetArticlesQueryResult
        {
            PagingInfo = request,
            TotalSize = articles[0].CountOver ?? 0,
            ArticleCategories = categories,
            Results = articles
        };
    }
}