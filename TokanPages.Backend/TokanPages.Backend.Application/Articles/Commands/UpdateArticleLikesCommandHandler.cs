using MediatR;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleLikesCommandHandler : RequestHandler<UpdateArticleLikesCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    private readonly IArticlesRepository _articlesRepository;

    private static BusinessException ArticleException => new(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

    public UpdateArticleLikesCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IUserService userService, 
    IDateTimeService dateTimeService, IArticlesRepository articlesRepository) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
        _articlesRepository = articlesRepository;
    }

    public override async Task<Unit> Handle(UpdateArticleLikesCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var ipAddress = _userService.GetRequestIpAddress();
        var isAnonymousUser = userId == Guid.Empty;
        var dateTimeStamp = _dateTimeService.Now;

        var isSuccess = await _articlesRepository.UpdateArticleLikes(userId, request.Id, dateTimeStamp, request.AddToLikes, isAnonymousUser, ipAddress, cancellationToken);

        return !isSuccess 
            ? throw ArticleException 
            : Unit.Value;
    }
}