namespace TokanPages.Backend.Application.Handlers.Commands.Articles;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Domain.Entities;
using Core.Exceptions;
using Core.Extensions;
using Shared.Resources;
using Services.UserService;
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;
using MediatR;

public class UpdateArticleCountCommandHandler : Application.RequestHandler<UpdateArticleCountCommand, Unit>
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