namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Database;
using Domain.Entities;
using Core.Exceptions;
using Shared.Resources;
using Services.UserService;
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;

public class UpdateArticleCountCommandHandler : Cqrs.RequestHandler<UpdateArticleCountCommand, Unit>
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

        article.ReadCount += 1;

        var user = await _userService.GetActiveUser(cancellationToken: cancellationToken);
        var articleCount = await DatabaseContext.ArticleCounts
            .Where(counts => counts.UserId == user.Id)
            .Where(counts => counts.ArticleId == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleCount is not null)
        {
            articleCount.ReadCount += 1;
            articleCount.ModifiedAt = _dateTimeService.Now;
            articleCount.ModifiedBy = user.Id;
        }
        else
        {
            var ipAddress = _userService.GetRequestIpAddress();
            var newArticleCount = new ArticleCounts
            {
                UserId = article.UserId,
                ArticleId = article.Id,
                IpAddress = ipAddress,
                ReadCount = 1,
                CreatedAt = _dateTimeService.Now,
                CreatedBy = user.Id,
                ModifiedAt = null,
                ModifiedBy = null
            };

            await DatabaseContext.ArticleCounts.AddAsync(newArticleCount, cancellationToken);
        }

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}