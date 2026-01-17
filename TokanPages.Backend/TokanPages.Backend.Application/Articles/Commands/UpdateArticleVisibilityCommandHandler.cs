using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleVisibilityCommandHandler : RequestHandler<UpdateArticleVisibilityCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    public UpdateArticleVisibilityCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IDateTimeService dateTimeService) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
    }

    public override async Task<Unit> Handle(UpdateArticleVisibilityCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var canPublishArticles = await _userService
            .HasPermissionAssigned(nameof(Permission.CanPublishArticles), cancellationToken: cancellationToken) ?? false;

        if (!canPublishArticles)
            throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

        var articleData = await OperationDbContext.Articles
            .Where(article => article.UserId == userId)
            .Where(article => article.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleData is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        articleData.IsPublished = request.IsPublished;
        articleData.ModifiedAt = _dateTimeService.Now;
        articleData.ModifiedBy = userId;

        await OperationDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}