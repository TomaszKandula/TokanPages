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

public class UpdateArticleCountCommandHandler : Cqrs.RequestHandler<UpdateArticleCountCommand, Unit>
{
    private readonly IUserService _userService;

    public UpdateArticleCountCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService) : base(databaseContext, loggerService) => _userService = userService;

    public override async Task<Unit> Handle(UpdateArticleCountCommand request, CancellationToken cancellationToken)
    {
        var article = await DatabaseContext.Articles
            .Where(articles => articles.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (article is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        article.ReadCount += 1;

        var user = await _userService.GetUser(cancellationToken);
        if (user != null)
        {
            var readCounts = await DatabaseContext.ArticleCounts
                .Where(counts => counts.UserId == user.UserId)
                .Where(counts => counts.ArticleId == request.Id)
                .SingleOrDefaultAsync(cancellationToken);

            if (readCounts != null)
            {
                readCounts.ReadCount += 1;
            }
            else
            {
                var ipAddress = _userService.GetRequestIpAddress();
                var articleCount = new ArticleCounts
                {
                    UserId = article.UserId,
                    ArticleId = article.Id,
                    IpAddress = ipAddress,
                    ReadCount = 1
                };
                await DatabaseContext.ArticleCounts.AddAsync(articleCount, cancellationToken);
            }
        }

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}