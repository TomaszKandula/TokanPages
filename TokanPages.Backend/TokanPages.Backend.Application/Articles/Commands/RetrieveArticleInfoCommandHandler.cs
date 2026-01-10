using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Application.Articles.Models;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
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
        var articleInfo = await (
            from articles in DatabaseContext.Articles
            join temp in 
                (from articleCategory in DatabaseContext.ArticleCategories
                    join categoryNames in DatabaseContext.CategoryNames
                        on articleCategory.Id equals categoryNames.ArticleCategoryId
                    join languages in DatabaseContext.Languages
                        on categoryNames.LanguageId equals languages.Id
                    select new
                    {
                        categoryNames.ArticleCategoryId,
                        categoryNames.Name,
                        languages.LangId
                    }
                )
            on articles.CategoryId equals temp.ArticleCategoryId
            where temp.LangId == userLanguage
            where articleIds.Contains(articles.Id)
            select new ArticleDataDto
            {
                Id = articles.Id,
                CategoryName = temp.Name,
                Title = articles.Title,
                Description = articles.Description,
                IsPublished = articles.IsPublished,
                ReadCount = articles.ReadCount,
                TotalLikes = articles.ArticleLikes
                    .Where(likes => likes.ArticleId == articles.Id)
                    .Select(likes => likes.LikeCount)
                    .Sum(),
                CreatedAt = articles.CreatedAt,
                UpdatedAt = articles.UpdatedAt,
                LanguageIso = articles.LanguageIso
            }).ToListAsync(cancellationToken);

        return new RetrieveArticleInfoCommandResult
        {
            Articles = articleInfo
        };
    }
}