using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleLikesCommandHandler : RequestHandler<UpdateArticleLikesCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    private readonly IConfiguration _configuration;

    public UpdateArticleLikesCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IUserService userService, 
    IDateTimeService dateTimeService, IConfiguration configuration) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
        _configuration = configuration;
    }

    public override async Task<Unit> Handle(UpdateArticleLikesCommand request, CancellationToken cancellationToken)
    {
        var articleData = await OperationDbContext.Articles
            .Where(article => article.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleData is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        var userId = _userService.GetLoggedUserId();
        var isAnonymousUser = userId == Guid.Empty;
        var ipAddress = _userService.GetRequestIpAddress();

        if (isAnonymousUser)
        {
            var articleLike = await OperationDbContext.ArticleLikes
                .Where(like => like.ArticleId == request.Id)
                .Where(like => like.IpAddress == ipAddress)
                .SingleOrDefaultAsync(cancellationToken);

            if (articleLike is null)
            {
                await AddLikes(userId, articleData, request, ipAddress, cancellationToken);
            }
            else
            {
                UpdateLikes(userId, articleData, articleLike, request.AddToLikes);
            }
        }
        else
        {
            var articleLike = await OperationDbContext.ArticleLikes
                .Where(like => like.ArticleId == request.Id)
                .Where(like => like.UserId == userId)
                .SingleOrDefaultAsync(cancellationToken);

            if (articleLike is null)
            {
                await AddLikes(userId, articleData, request, ipAddress, cancellationToken);
            }
            else
            {
                UpdateLikes(userId, articleData, articleLike, request.AddToLikes);
            }
        }

        await OperationDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

    private async Task AddLikes(Guid userId, Article article, UpdateArticleLikesCommand request, string ipAddress, CancellationToken cancellationToken)
    {
        var likesLimit = userId == Guid.Empty
            ? _configuration.GetValue<int>("Limit_Likes_Anonymous")
            : _configuration.GetValue<int>("Limit_Likes_User");

        var likes = request.AddToLikes > likesLimit ? likesLimit : request.AddToLikes;
        var entity = new ArticleLike
        {
            ArticleId = request.Id,
            UserId = userId == Guid.Empty ? null : userId,
            IpAddress = ipAddress,
            LikeCount = likes,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = userId,
            ModifiedAt = null,
            ModifiedBy = null
        };

        article.TotalLikes += likes;
        article.ModifiedAt = _dateTimeService.Now;
        article.ModifiedBy = userId == Guid.Empty ? null : userId;
        await OperationDbContext.ArticleLikes.AddAsync(entity, cancellationToken);
    }

    private void UpdateLikes(Guid? userId, Article article, ArticleLike articleLike, int likesToBeAdded)
    {
        var likesLimit = userId == Guid.Empty
            ? _configuration.GetValue<int>("Limit_Likes_Anonymous") 
            : _configuration.GetValue<int>("Limit_Likes_User");

        var likes = likesToBeAdded > likesLimit ? likesLimit : likesToBeAdded;
        articleLike.LikeCount += likes;
        articleLike.ModifiedAt = _dateTimeService.Now;
        articleLike.ModifiedBy = userId == Guid.Empty ? null : userId;

        article.TotalLikes += likes;
        article.ModifiedAt = _dateTimeService.Now;
        article.ModifiedBy = userId == Guid.Empty ? null : userId;
        OperationDbContext.ArticleLikes.Update(articleLike);
    }
}