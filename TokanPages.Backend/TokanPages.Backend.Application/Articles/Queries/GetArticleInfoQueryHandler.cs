using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticleInfoQueryHandler : RequestHandler<GetArticleInfoQuery, GetArticleInfoQueryResult>
{
    private readonly IUserService _userService;

    public GetArticleInfoQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, IUserService userService) 
        : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<GetArticleInfoQueryResult> Handle(GetArticleInfoQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUser(cancellationToken);
        var isAnonymousUser = user == null;

        var requestId = Guid.Empty;
        if (request.Id is not null)
            requestId = (Guid)request.Id;

        var userLikes = await DatabaseContext.ArticleLikes
            .AsNoTracking()
            .Where(likes => likes.ArticleId == requestId)
            .WhereIfElse(isAnonymousUser,
                likes => likes.IpAddress == _userService.GetRequestIpAddress(),
                likes => likes.UserId == user!.UserId)
            .Select(likes => likes.LikeCount)
            .SumAsync(cancellationToken);

        var totalLikes = await DatabaseContext.ArticleLikes
            .AsNoTracking()
            .Where(likes => likes.ArticleId == requestId)
            .Select(likes => likes.LikeCount)
            .SumAsync(cancellationToken);

        var query = await (from articles in DatabaseContext.Articles
            join userInfo in DatabaseContext.UserInfo
            on articles.UserId equals userInfo.UserId
            join users in DatabaseContext.Users
            on articles.UserId equals users.Id
            where articles.Id == requestId
            select new GetArticleQueryResult
            {
                Id = articles.Id,
                Title = articles.Title,
                Description = articles.Description,
                IsPublished = articles.IsPublished,
                CreatedAt = articles.CreatedAt,
                UpdatedAt = articles.UpdatedAt,
                ReadCount = articles.ReadCount,
                LanguageIso = articles.LanguageIso,
                LikeCount = totalLikes,
                UserLikes = userLikes
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        if (query is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        return new GetArticleInfoQueryResult
        {
            Id = query.Id,
            Title = query.Title,
            Description = query.Description,
            IsPublished = query.IsPublished,
            CreatedAt = query.CreatedAt,
            UpdatedAt = query.UpdatedAt,
            ReadCount = query.ReadCount,
            LanguageIso = query.LanguageIso,
            LikeCount = query.LikeCount,
            UserLikes = query.UserLikes
        };
    }
}