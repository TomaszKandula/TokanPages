namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared;
using Database;
using Core.Exceptions;
using Core.Extensions;
using Shared.Resources;
using Services.UserService;
using Core.Utilities.LoggerService;
using MediatR;

public class UpdateArticleLikesCommandHandler : Cqrs.RequestHandler<UpdateArticleLikesCommand, Unit>
{
    private readonly IUserService _userService;
        
    public UpdateArticleLikesCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService) : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<Unit> Handle(UpdateArticleLikesCommand request, CancellationToken cancellationToken)
    {
        var articles = await DatabaseContext.Articles
            .AsNoTracking()
            .Where(articles => articles.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (articles is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        var userId = await _userService.GetUserId(cancellationToken);
        var isAnonymousUser = userId == null;

        var articleLikes = await DatabaseContext.ArticleLikes
            .Where(likes => likes.ArticleId == request.Id)
            .WhereIfElse(isAnonymousUser,
                likes => likes.IpAddress == _userService.GetRequestIpAddress(),
                likes => likes.UserId == userId)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleLikes is null)
        {
            await AddNewArticleLikes(isAnonymousUser, request, cancellationToken);
        }
        else
        {
            UpdateCurrentArticleLikes(isAnonymousUser, articleLikes, request.AddToLikes);
        }

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
        
    private async Task AddNewArticleLikes(bool isAnonymousUser, UpdateArticleLikesCommand request, CancellationToken cancellationToken)
    {
        var likesLimit = isAnonymousUser 
            ? Constants.Likes.LikesLimitForAnonymous 
            : Constants.Likes.LikesLimitForUser;

        var userId = await _userService.GetUserId(cancellationToken);
        var ipAddress = _userService.GetRequestIpAddress();

        var entity = new Domain.Entities.ArticleLikes
        {
            ArticleId = request.Id,
            UserId = userId,
            IpAddress = ipAddress,
            LikeCount = request.AddToLikes > likesLimit ? likesLimit : request.AddToLikes
        };

        await DatabaseContext.ArticleLikes.AddAsync(entity, cancellationToken);
    }

    private static void UpdateCurrentArticleLikes(bool isAnonymousUser, Domain.Entities.ArticleLikes entity, int likesToBeAdded)
    {
        var likesLimit = isAnonymousUser 
            ? Constants.Likes.LikesLimitForAnonymous 
            : Constants.Likes.LikesLimitForUser;

        var sum = entity.LikeCount + likesToBeAdded;
        entity.LikeCount = sum > likesLimit ? likesLimit : sum;
    }
}