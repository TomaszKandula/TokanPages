using MediatR;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleVisibilityCommandHandler : RequestHandler<UpdateArticleVisibilityCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IArticlesRepository _articlesRepository;

    private const string Permission = nameof(Domain.Enums.Permission.CanPublishArticles);

    private static BusinessException ArticleException => new(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

    public UpdateArticleVisibilityCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IArticlesRepository articlesRepository) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _articlesRepository = articlesRepository;
    }

    public override async Task<Unit> Handle(UpdateArticleVisibilityCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var canPublishArticles = await _userService
            .HasPermissionAssigned(Permission, cancellationToken: cancellationToken) ?? false;

        if (!canPublishArticles)
            throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

        var isSuccess = await _articlesRepository.UpdateArticleVisibility(userId, request.Id, request.IsPublished);

        return !isSuccess 
            ? throw ArticleException 
            : Unit.Value;
    }
}