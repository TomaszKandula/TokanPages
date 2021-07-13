namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Exceptions;
    using Shared.Resources;
    using Identity.Authorization;
    using Services.UserServiceProvider;
    using MediatR;

    public class UpdateArticleVisibilityCommandHandler : TemplateHandler<UpdateArticleVisibilityCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        private readonly IUserServiceProvider FUserServiceProvider;
        
        public UpdateArticleVisibilityCommandHandler(DatabaseContext ADatabaseContext, IUserServiceProvider AUserServiceProvider)
        {
            FDatabaseContext = ADatabaseContext;
            FUserServiceProvider = AUserServiceProvider;
        }
        
        public override async Task<Unit> Handle(UpdateArticleVisibilityCommand ARequest, CancellationToken ACancellationToken)
        {
            var LCanPublishArticles = await FUserServiceProvider
                .HasPermissionAssigned(nameof(Permissions.CanPublishArticles)) ?? false;
            
            if (!LCanPublishArticles)
                throw new BusinessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

            var LArticles = await FDatabaseContext.Articles
                .Where(AArticles => AArticles.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LArticles.Any())
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

            var LCurrentArticle = LArticles.First();
            LCurrentArticle.IsPublished = ARequest.IsPublished;

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}