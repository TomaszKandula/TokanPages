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
    using Services.UserServiceProvider;

    public class UpdateArticleCountCommandHandler : TemplateHandler<UpdateArticleCountCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        private readonly IUserServiceProvider FUserServiceProvider;

        public UpdateArticleCountCommandHandler(DatabaseContext ADatabaseContext, IUserServiceProvider AUserServiceProvider)
        {
            FDatabaseContext = ADatabaseContext;
            FUserServiceProvider = AUserServiceProvider;
        }

        public override async Task<Unit> Handle(UpdateArticleCountCommand ARequest, CancellationToken ACancellationToken)
        {
            var LArticles = await FDatabaseContext.Articles
                .Where(AArticles => AArticles.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LArticles.Any())
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

            var LCurrentArticle = LArticles.First();
            LCurrentArticle.ReadCount += 1;

            var LUserId = await FUserServiceProvider.GetUserId();
            if (LUserId != null)
            {
                var LReadCounts = await FDatabaseContext.ArticleCounts
                    .Where(ACounts => ACounts.UserId == LUserId && ACounts.ArticleId == ARequest.Id)
                    .SingleOrDefaultAsync(ACancellationToken);

                if (LReadCounts != null)
                {
                    LReadCounts.ReadCount += 1;
                }
                else
                {
                    var LIpAddress = FUserServiceProvider.GetRequestIpAddress();
                    var LArticleCount = new ArticleCounts
                    {
                        UserId = LCurrentArticle.UserId,
                        ArticleId = LCurrentArticle.Id,
                        IpAddress = LIpAddress,
                        ReadCount = 1
                    };
                    await FDatabaseContext.ArticleCounts.AddAsync(LArticleCount, ACancellationToken);
                }
            }

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}