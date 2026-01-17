using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleCountCommandHandler : RequestHandler<UpdateArticleCountCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    public UpdateArticleCountCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IDateTimeService dateTimeService) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
    }

    public override async Task<Unit> Handle(UpdateArticleCountCommand request, CancellationToken cancellationToken)
    {
        var articleData = await OperationDbContext.Articles
            .Where(article => article.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleData is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        var userId = _userService.GetLoggedUserId();
        var ipAddress = _userService.GetRequestIpAddress();
        var articleCount = await OperationDbContext.ArticleCounts
            .Where(count => count.ArticleId == request.Id)
            .Where(count => count.IpAddress == ipAddress)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleCount is null)
        {
            var newArticleCount = new ArticleCount
            {
                UserId = articleData.UserId,
                ArticleId = articleData.Id,
                IpAddress = ipAddress,
                ReadCount = 1,
                CreatedAt = _dateTimeService.Now,
                CreatedBy = userId,
                ModifiedAt = null,
                ModifiedBy = null
            };

            await OperationDbContext.ArticleCounts.AddAsync(newArticleCount, cancellationToken);
        }
        else
        {
            articleCount.ReadCount += 1;
            articleCount.ModifiedAt = _dateTimeService.Now;
            articleCount.ModifiedBy = userId;

            OperationDbContext.ArticleCounts.Update(articleCount);
        }

        articleData.ReadCount += 1;
        articleData.ModifiedAt = _dateTimeService.Now;
        articleData.ModifiedBy = userId;

        await OperationDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}