using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Article;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleLikesCommandHandler : RequestHandler<UpdateArticleLikesCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    private readonly IConfiguration _configuration;
    
    public UpdateArticleLikesCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, IUserService userService, 
    IDateTimeService dateTimeService, IConfiguration configuration) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
        _configuration = configuration;
    }

    public override async Task<Unit> Handle(UpdateArticleLikesCommand request, CancellationToken cancellationToken)
    {
        var articles = await DatabaseContext.Articles
            .Where(articles => articles.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (articles is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        var user = await _userService.GetUser(cancellationToken);
        var isAnonymousUser = user == null;
        var ipAddress = _userService.GetRequestIpAddress();

        if (isAnonymousUser)
        {
            var articleLikes = await DatabaseContext.ArticleLikes
                .Where(likes => likes.ArticleId == request.Id)
                .Where(likes => likes.IpAddress == ipAddress)
                .SingleOrDefaultAsync(cancellationToken);

            if (articleLikes is null)
            {
                await AddLikes(user?.UserId, articles, request, ipAddress, cancellationToken);
            }
            else
            {
                UpdateLikes(user?.UserId, articles, articleLikes, request.AddToLikes);
            }
        }
        else
        {
            var articleLikes = await DatabaseContext.ArticleLikes
                .Where(likes => likes.ArticleId == request.Id)
                .Where(likes => likes.UserId == user!.UserId)
                .SingleOrDefaultAsync(cancellationToken);

            if (articleLikes is null)
            {
                await AddLikes(user?.UserId, articles, request, ipAddress, cancellationToken);
            }
            else
            {
                UpdateLikes(user?.UserId, articles, articleLikes, request.AddToLikes);
            }
        }

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

    private async Task AddLikes(Guid? userId, Domain.Entities.Article.Articles articles, UpdateArticleLikesCommand request, string ipAddress, CancellationToken cancellationToken)
    {
        var likesLimit = userId == null
            ? _configuration.GetValue<int>("Limit_Likes_Anonymous")
            : _configuration.GetValue<int>("Limit_Likes_User");

        var likes = request.AddToLikes > likesLimit ? likesLimit : request.AddToLikes;
        var entity = new ArticleLikes
        {
            ArticleId = request.Id,
            UserId = userId,
            IpAddress = ipAddress,
            LikeCount = likes,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = userId ?? Guid.Empty,
            ModifiedAt = null,
            ModifiedBy = null
        };

        articles.TotalLikes += likes;
        articles.UpdatedAt = _dateTimeService.Now;
        await DatabaseContext.ArticleLikes.AddAsync(entity, cancellationToken);
    }

    private void UpdateLikes(Guid? userId, Domain.Entities.Article.Articles articles, ArticleLikes articleLikes, int likesToBeAdded)
    {
        var likesLimit = userId == null 
            ? _configuration.GetValue<int>("Limit_Likes_Anonymous") 
            : _configuration.GetValue<int>("Limit_Likes_User");

        var likes = likesToBeAdded > likesLimit ? likesLimit : likesToBeAdded;
        articleLikes.LikeCount += likes;
        articleLikes.ModifiedAt = _dateTimeService.Now;
        articleLikes.ModifiedBy = userId;

        articles.TotalLikes += likes;
        articles.UpdatedAt = _dateTimeService.Now;
        DatabaseContext.ArticleLikes.Update(articleLikes);
    }
}