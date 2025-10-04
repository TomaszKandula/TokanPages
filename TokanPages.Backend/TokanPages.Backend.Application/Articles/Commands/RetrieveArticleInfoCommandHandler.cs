using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Application.Articles.Models;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class RetrieveArticleInfoCommandHandler : RequestHandler<RetrieveArticleInfoCommand, RetrieveArticleInfoCommandResult>
{
    public RetrieveArticleInfoCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, IUserService userService)
        : base(databaseContext, loggerService) { }

    public override async Task<RetrieveArticleInfoCommandResult> Handle(RetrieveArticleInfoCommand request, CancellationToken cancellationToken)
    {
        var articleIds = new HashSet<Guid>(request.ArticleIds);
        var articleInfo = await DatabaseContext.Articles
            .AsNoTracking()
            .Include(articles => articles.ArticleCategory)
            .Include(articles => articles.ArticleLikes)
            .Where(articles => articleIds.Contains(articles.Id))
            .Select(articles => new ArticleDataDto
            {
                Id = articles.Id,
                CategoryName = articles.ArticleCategory.CategoryName,
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
            })
            .ToListAsync(cancellationToken);

        return new RetrieveArticleInfoCommandResult
        {
            Articles = articleInfo
        };
    }
}