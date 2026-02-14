using MediatR;
using Microsoft.Extensions.Options;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleLikesCommandHandler : RequestHandler<UpdateArticleLikesCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IArticlesRepository _articlesRepository;

    private readonly AppSettingsModel _appSettings;

    public UpdateArticleLikesCommandHandler(ILoggerService loggerService, IUserService userService, 
    IArticlesRepository articlesRepository, IOptions<AppSettingsModel> options) : base(loggerService)
    {
        _userService = userService;
        _articlesRepository = articlesRepository;
        _appSettings = options.Value;
    }

    public override async Task<Unit> Handle(UpdateArticleLikesCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var ipAddress = _userService.GetRequestIpAddress();
        var isAnonymousUser = userId == Guid.Empty;

        var likesLimit = userId == Guid.Empty ? _appSettings.LimitLikesAnonymous : _appSettings.LimitLikesUser;
        var likes = request.AddToLikes > likesLimit ? likesLimit : request.AddToLikes;

        var articleLikes = await _articlesRepository.GetArticleLikes(isAnonymousUser, userId, request.Id, ipAddress);
        if (articleLikes is null)
        {
            await _articlesRepository.CreateArticleLikes(userId, request.Id, ipAddress, likes);
        }
        else
        {
            var likesToBeAdded = articleLikes.LikeCount + likes;
            await _articlesRepository.UpdateArticleLikes(userId, request.Id, likesToBeAdded, isAnonymousUser, ipAddress);
        }

        return Unit.Value;
    }
}