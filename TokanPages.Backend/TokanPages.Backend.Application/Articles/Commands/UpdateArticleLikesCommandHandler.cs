using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities;
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
            .AsNoTracking()
            .Where(articles => articles.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (articles is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        var user = await _userService.GetUser(cancellationToken);
        var articleLikes = await DatabaseContext.ArticleLikes
            .Where(likes => likes.ArticleId == request.Id)
            .WhereIfElse(user == null,
                likes => likes.IpAddress == _userService.GetRequestIpAddress(),
                likes => likes.UserId == user!.UserId)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleLikes is null)
        {
            await AddLikes(user?.UserId, request, cancellationToken);
        }
        else
        {
            UpdateLikes(user?.UserId, articleLikes, request.AddToLikes);
        }

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }

    private async Task AddLikes(Guid? userId, UpdateArticleLikesCommand request, CancellationToken cancellationToken)
    {
        var likesLimit = userId == null
            ? _configuration.GetValue<int>("Limit_Likes_Anonymous")
            : _configuration.GetValue<int>("Limit_Likes_User");

        var entity = new ArticleLikes
        {
            ArticleId = request.Id,
            UserId = userId,
            IpAddress = _userService.GetRequestIpAddress(),
            LikeCount = request.AddToLikes > likesLimit ? likesLimit : request.AddToLikes,
            CreatedAt = _dateTimeService.Now,
            CreatedBy = userId ?? Guid.Empty,
            ModifiedAt = null,
            ModifiedBy = null
        };

        await DatabaseContext.ArticleLikes.AddAsync(entity, cancellationToken);
    }

    private void UpdateLikes(Guid? userId, ArticleLikes entity, int likesToBeAdded)
    {
        var likesLimit = userId == null 
            ? _configuration.GetValue<int>("Limit_Likes_Anonymous") 
            : _configuration.GetValue<int>("Limit_Likes_User");

        var sum = entity.LikeCount + likesToBeAdded;
        entity.LikeCount = sum > likesLimit ? likesLimit : sum;
        entity.ModifiedAt = _dateTimeService.Now;
        entity.ModifiedBy = userId;

        DatabaseContext.ArticleLikes.Update(entity);
    }
}