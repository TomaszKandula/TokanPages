using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Identity.Authorization;
using TokanPages.Backend.Cqrs.Services.UserProvider;
using MediatR;

namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    public class UpdateArticleVisibilityCommandHandler : TemplateHandler<UpdateArticleVisibilityCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        private readonly IUserProvider FUserProvider;
        
        public UpdateArticleVisibilityCommandHandler(DatabaseContext ADatabaseContext, IUserProvider AUserProvider)
        {
            FDatabaseContext = ADatabaseContext;
            FUserProvider = AUserProvider;
        }
        
        public override async Task<Unit> Handle(UpdateArticleVisibilityCommand ARequest, CancellationToken ACancellationToken)
        {
            var LCanPublishArticles = await FUserProvider
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