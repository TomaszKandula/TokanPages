using MediatR;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class RemoveArticleCommandHandler : RequestHandler<RemoveArticleCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IArticlesRepository _articlesRepository;

    private static BusinessException ArticleException => new(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

    public RemoveArticleCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IArticlesRepository articlesRepository) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _articlesRepository = articlesRepository;
    }

    public override async Task<Unit> Handle(RemoveArticleCommand request, CancellationToken cancellationToken) 
    {
        var userId = _userService.GetLoggedUserId();
        var isSuccess = await _articlesRepository.RemoveArticle(userId, request.Id, cancellationToken);

        return !isSuccess 
            ? throw ArticleException 
            : Unit.Value;
    }
}