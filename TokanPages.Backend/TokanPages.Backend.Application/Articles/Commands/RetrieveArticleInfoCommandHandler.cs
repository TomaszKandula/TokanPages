using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class RetrieveArticleInfoCommandHandler : RequestHandler<RetrieveArticleInfoCommand, RetrieveArticleInfoCommandResult>
{
    private readonly IUserService _userService;

    private readonly IArticlesRepository _articlesRepository;

    public RetrieveArticleInfoCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IUserService userService, 
        IArticlesRepository articlesRepository)
        : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _articlesRepository = articlesRepository;
    }

    public override async Task<RetrieveArticleInfoCommandResult> Handle(RetrieveArticleInfoCommand request, CancellationToken cancellationToken)
    {
        var userLanguage = _userService.GetRequestUserLanguage();
        var articleIds = new HashSet<Guid>(request.ArticleIds);
        var articleInfoList = await _articlesRepository.RetrieveArticleInfo(userLanguage, articleIds, cancellationToken);

        return new RetrieveArticleInfoCommandResult
        {
            Articles = articleInfoList
        };
    }
}