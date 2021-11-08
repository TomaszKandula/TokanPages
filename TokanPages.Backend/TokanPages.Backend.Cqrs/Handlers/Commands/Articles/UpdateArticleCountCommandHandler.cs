namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    using MediatR;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Domain.Entities;
    using Core.Exceptions;
    using Shared.Resources;
    using Core.Utilities.LoggerService;
    using Services.UserServiceProvider;

    public class UpdateArticleCountCommandHandler : TemplateHandler<UpdateArticleCountCommand, Unit>
    {
        private readonly IUserServiceProvider _userServiceProvider;

        public UpdateArticleCountCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, IUserServiceProvider userServiceProvider) : base(databaseContext, loggerService)
        {
            _userServiceProvider = userServiceProvider;
        }

        public override async Task<Unit> Handle(UpdateArticleCountCommand request, CancellationToken cancellationToken)
        {
            var articles = await DatabaseContext.Articles
                .Where(articles => articles.Id == request.Id)
                .ToListAsync(cancellationToken);

            if (!articles.Any())
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

            var currentArticle = articles.First();
            currentArticle.ReadCount += 1;

            var userId = await _userServiceProvider.GetUserId();
            if (userId != null)
            {
                var readCounts = await DatabaseContext.ArticleCounts
                    .Where(counts => counts.UserId == userId && counts.ArticleId == request.Id)
                    .SingleOrDefaultAsync(cancellationToken);

                if (readCounts != null)
                {
                    readCounts.ReadCount += 1;
                }
                else
                {
                    var ipAddress = _userServiceProvider.GetRequestIpAddress();
                    var articleCount = new ArticleCounts
                    {
                        UserId = currentArticle.UserId,
                        ArticleId = currentArticle.Id,
                        IpAddress = ipAddress,
                        ReadCount = 1
                    };
                    await DatabaseContext.ArticleCounts.AddAsync(articleCount, cancellationToken);
                }
            }

            await DatabaseContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}