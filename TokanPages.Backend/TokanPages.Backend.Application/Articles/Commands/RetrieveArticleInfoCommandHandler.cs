using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Application.Articles.Models;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class RetrieveArticleInfoCommandHandler : RequestHandler<RetrieveArticleInfoCommand, RetrieveArticleInfoCommandResult>
{
    private readonly IUserService _userService;

    public RetrieveArticleInfoCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, IUserService userService)
        : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<RetrieveArticleInfoCommandResult> Handle(RetrieveArticleInfoCommand request, CancellationToken cancellationToken)
    {
        var userLanguage = _userService.GetRequestUserLanguage();
        var articleIds = new HashSet<Guid>(request.ArticleIds);
        var articleInfoList = await (
            from article in DatabaseContext.Articles
            join table in 
                (from articleCategory in DatabaseContext.ArticleCategories
                    join categoryName in DatabaseContext.CategoryNames
                        on articleCategory.Id equals categoryName.ArticleCategoryId
                    join language in DatabaseContext.Languages
                        on categoryName.LanguageId equals language.Id
                    select new
                    {
                        categoryName.ArticleCategoryId,
                        categoryName.Name,
                        language.LangId
                    }
                )
            on article.CategoryId equals table.ArticleCategoryId
            where table.LangId == userLanguage
            where articleIds.Contains(article.Id)
            select new ArticleDataDto
            {
                Id = article.Id,
                CategoryName = table.Name,
                Title = article.Title,
                Description = article.Description,
                IsPublished = article.IsPublished,
                ReadCount = article.ReadCount,
                TotalLikes = article.ArticleLikes
                    .Where(likes => likes.ArticleId == article.Id)
                    .Select(likes => likes.LikeCount)
                    .Sum(),
                CreatedAt = article.CreatedAt,
                UpdatedAt = article.UpdatedAt,
                LanguageIso = article.LanguageIso
            }).ToListAsync(cancellationToken);

        return new RetrieveArticleInfoCommandResult
        {
            Articles = articleInfoList
        };
    }
}