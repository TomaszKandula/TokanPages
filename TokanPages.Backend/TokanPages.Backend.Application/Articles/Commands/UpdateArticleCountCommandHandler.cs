using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleCountCommandHandler : RequestHandler<UpdateArticleCountCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    public UpdateArticleCountCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService, IDateTimeService dateTimeService) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
    }

    public override async Task<Unit> Handle(UpdateArticleCountCommand request, CancellationToken cancellationToken)
    {
        var article = await DatabaseContext.Articles
            .Where(articles => articles.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (article is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        var user = await _userService.GetUser(cancellationToken);
        var articleCount = await DatabaseContext.ArticleCounts
            .Where(counts => counts.ArticleId == request.Id)
            .WhereIfElse(user == null,
                counts => counts.IpAddress == _userService.GetRequestIpAddress(),
                counts => counts.UserId == user!.UserId)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleCount is null)
        {
            var ipAddress = _userService.GetRequestIpAddress();
            var newArticleCount = new ArticleCounts
            {
                UserId = article.UserId,
                ArticleId = article.Id,
                IpAddress = ipAddress,
                ReadCount = 1,
                CreatedAt = _dateTimeService.Now,
                CreatedBy = user?.UserId ?? Guid.Empty,
                ModifiedAt = null,
                ModifiedBy = null
            };

            await DatabaseContext.ArticleCounts.AddAsync(newArticleCount, cancellationToken);
        }
        else
        {
            articleCount.ReadCount += 1;
            articleCount.ModifiedAt = _dateTimeService.Now;
            articleCount.ModifiedBy = user?.UserId;

            DatabaseContext.ArticleCounts.Update(articleCount);
        }

        article.ReadCount += 1;
        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}