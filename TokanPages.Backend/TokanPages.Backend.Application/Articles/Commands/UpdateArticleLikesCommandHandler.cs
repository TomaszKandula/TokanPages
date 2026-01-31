using MediatR;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Configuration.Options;
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

    private readonly AppSettings _appSettings;

    public UpdateArticleLikesCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IUserService userService, 
    IDateTimeService dateTimeService, IArticlesRepository articlesRepository, IOptions<AppSettings> options) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
        _articlesRepository = articlesRepository;
        _appSettings = options.Value;
    }

    public override async Task<Unit> Handle(UpdateArticleLikesCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var ipAddress = _userService.GetRequestIpAddress();
        var isAnonymousUser = userId == Guid.Empty;
        var dateTimeStamp = _dateTimeService.Now;

        var likesLimit = userId == Guid.Empty ? _appSettings.LimitLikesAnonymous : _appSettings.LimitLikesUser;
        var likes = request.AddToLikes > likesLimit ? likesLimit : request.AddToLikes;

        bool isSuccess;
        var articleLikes = await _articlesRepository.GetArticleLikes(isAnonymousUser, userId, request.Id, ipAddress);
        if (articleLikes is null)
        {
            isSuccess = await _articlesRepository.CreateArticleLikes(userId, request.Id, ipAddress, likes, dateTimeStamp, cancellationToken);
        }
        else
        {
            var likesToBeAdded = articleLikes.LikeCount + likes;
            isSuccess = await _articlesRepository.UpdateArticleLikes(userId, request.Id, dateTimeStamp, likesToBeAdded, isAnonymousUser, ipAddress, cancellationToken);
        }

        return !isSuccess 
            ? throw ArticleException 
            : Unit.Value;
    }
}