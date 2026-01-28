using MediatR;
using Microsoft.Extensions.Configuration;
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

    //TODO: replace IConfiguration with IOption
    private readonly IConfiguration _configuration;
    private int MaxLikesForAnonymousUser => _configuration.GetValue<int>("Limit_Likes_Anonymous");
    private int MaxLikesForLoggedUser => _configuration.GetValue<int>("Limit_Likes_User");

    public UpdateArticleLikesCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IUserService userService, 
    IDateTimeService dateTimeService, IArticlesRepository articlesRepository, IConfiguration configuration) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
        _articlesRepository = articlesRepository;
        _configuration = configuration;
    }

    public override async Task<Unit> Handle(UpdateArticleLikesCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var ipAddress = _userService.GetRequestIpAddress();
        var isAnonymousUser = userId == Guid.Empty;
        var dateTimeStamp = _dateTimeService.Now;

        var likesLimit = userId == Guid.Empty ? MaxLikesForAnonymousUser : MaxLikesForLoggedUser;
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