namespace TokanPages.Backend.Application.Articles.Commands;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Domain.Enums;
using Core.Exceptions;
using Shared.Resources;
using Services.UserService;
using Core.Utilities.LoggerService;
using MediatR;

public class UpdateArticleVisibilityCommandHandler : Application.RequestHandler<UpdateArticleVisibilityCommand, Unit>
{
    private readonly IUserService _userService;
        
    public UpdateArticleVisibilityCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService) : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<Unit> Handle(UpdateArticleVisibilityCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(null, false, cancellationToken);
        var canPublishArticles = await _userService
            .HasPermissionAssigned(nameof(Permissions.CanPublishArticles), cancellationToken: cancellationToken) ?? false;

        if (!canPublishArticles)
            throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

        var articles = await DatabaseContext.Articles
            .Where(articles => articles.UserId == user.Id)
            .Where(articles => articles.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (articles is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        articles.IsPublished = request.IsPublished;
        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}