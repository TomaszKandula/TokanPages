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

        var article = await (from articles in DatabaseContext.Articles
            join articleCategory in DatabaseContext.ArticleCategory 
            on articles.CategoryId equals articleCategory.Id
            where articles.Id == requestId
            select new 
            {
                articles.Id,
                articles.UserId,
                articles.Title,
                articles.Description,
                articles.IsPublished,
                articles.CreatedAt,
                articles.UpdatedAt,
                articles.ReadCount,
                articles.LanguageIso,
                articleCategory.CategoryName,
                TotalLikes = totalLikes,
                UserLikes = userLikes
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        if (article is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        return new GetArticleInfoQueryResult
        {
            Id = article.Id,
            Title = article.Title,
            CategoryName = article.CategoryName,
            Description = article.Description,
            IsPublished = article.IsPublished,
            CreatedAt = article.CreatedAt,
            UpdatedAt = article.UpdatedAt,
            ReadCount = article.ReadCount,
            LanguageIso = article.LanguageIso,
            TotalLikes = article.TotalLikes,
            UserLikes = article.UserLikes
        };
    }
}