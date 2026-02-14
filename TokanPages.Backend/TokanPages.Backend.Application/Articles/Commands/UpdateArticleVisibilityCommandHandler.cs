using MediatR;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleVisibilityCommandHandler : RequestHandler<UpdateArticleVisibilityCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IArticlesRepository _articlesRepository;

    private const string Permission = nameof(Domain.Enums.Permission.CanPublishArticles);

    public UpdateArticleVisibilityCommandHandler(ILoggerService loggerService, 
        IUserService userService, IArticlesRepository articlesRepository) : base(loggerService)
    {
        _userService = userService;
        _articlesRepository = articlesRepository;
    }

    public override async Task<Unit> Handle(UpdateArticleVisibilityCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var canPublishArticles = await _userService.HasPermissionAssigned(Permission) ?? false;

        if (!canPublishArticles)
            throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

        await _articlesRepository.UpdateArticleVisibility(userId, request.Id, request.IsPublished);

        return Unit.Value;
    }
}