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
        private readonly DatabaseContext _databaseContext;

        private readonly IUserServiceProvider _userServiceProvider;
        
        public UpdateArticleVisibilityCommandHandler(DatabaseContext databaseContext, IUserServiceProvider userServiceProvider)
        {
            _databaseContext = databaseContext;
            _userServiceProvider = userServiceProvider;
        }
        
        public override async Task<Unit> Handle(UpdateArticleVisibilityCommand request, CancellationToken cancellationToken)
        {
            var canPublishArticles = await _userServiceProvider
                .HasPermissionAssigned(nameof(Permissions.CanPublishArticles)) ?? false;
            
            if (!canPublishArticles)
                throw new BusinessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

            var articles = await _databaseContext.Articles
                .Where(articles => articles.Id == request.Id)
                .ToListAsync(cancellationToken);

            if (!articles.Any())
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

            var currentArticle = articles.First();
            currentArticle.IsPublished = request.IsPublished;

            await _databaseContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}