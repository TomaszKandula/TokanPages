namespace TokanPages.Backend.Application.Articles.Commands;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Domain.Entities;
using Core.Exceptions;
using Core.Extensions;
using Shared.Services;
using Shared.Resources;
using Services.UserService;
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;
using MediatR;

public class UpdateArticleLikesCommandHandler : Application.RequestHandler<UpdateArticleLikesCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    private readonly IApplicationSettings _applicationSettings;
    
    public UpdateArticleLikesCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, IUserService userService, 
    IDateTimeService dateTimeService, IApplicationSettings applicationSettings) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
        _applicationSettings = applicationSettings;
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
            ? _applicationSettings.LimitSettings.Likes.ForAnonymous 
            : _applicationSettings.LimitSettings.Likes.ForUser;

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
            ? _applicationSettings.LimitSettings.Likes.ForAnonymous 
            : _applicationSettings.LimitSettings.Likes.ForUser;

        var sum = entity.LikeCount + likesToBeAdded;
        entity.LikeCount = sum > likesLimit ? likesLimit : sum;
        entity.ModifiedAt = _dateTimeService.Now;
        entity.ModifiedBy = userId;

        DatabaseContext.ArticleLikes.Update(entity);
    }
}