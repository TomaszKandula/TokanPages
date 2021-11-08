namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Shared;
    using Database;
    using Core.Exceptions;
    using Core.Extensions;
    using Shared.Resources;
    using Core.Utilities.LoggerService;
    using Services.UserServiceProvider;
    using MediatR;

    public class UpdateArticleLikesCommandHandler : TemplateHandler<UpdateArticleLikesCommand, Unit>
    {
        private readonly IUserServiceProvider _userServiceProvider;
        
        public UpdateArticleLikesCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
            IUserServiceProvider userServiceProvider) : base(databaseContext, loggerService)
        {
            _userServiceProvider = userServiceProvider;
        }

        public override async Task<Unit> Handle(UpdateArticleLikesCommand request, CancellationToken cancellationToken)
        {
            var articles = await DatabaseContext.Articles
                .Where(articles => articles.Id == request.Id)
                .ToListAsync(cancellationToken);

            if (!articles.Any())
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

            var userId = await _userServiceProvider.GetUserId();
            var isAnonymousUser = userId == null;
            
            var articleLikes = await DatabaseContext.ArticleLikes
                .Where(likes => likes.ArticleId == request.Id)
                .WhereIfElse(isAnonymousUser,
                    likes => likes.IpAddress == _userServiceProvider.GetRequestIpAddress(),
                    likes => likes.UserId == userId)
                .ToListAsync(cancellationToken);

            if (!articleLikes.Any())
            {
                await AddNewArticleLikes(isAnonymousUser, request, cancellationToken);
            }
            else
            {
                UpdateCurrentArticleLikes(isAnonymousUser, articleLikes.First(), request.AddToLikes);
            }

            await DatabaseContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(Unit.Value);
        }
        
        private async Task AddNewArticleLikes(bool isAnonymousUser, UpdateArticleLikesCommand request, CancellationToken cancellationToken)
        {
            var likesLimit = isAnonymousUser 
                ? Constants.Likes.LikesLimitForAnonymous 
                : Constants.Likes.LikesLimitForUser;

            var entity = new Domain.Entities.ArticleLikes
            {
                ArticleId = request.Id,
                UserId = await _userServiceProvider.GetUserId(),
                IpAddress = _userServiceProvider.GetRequestIpAddress(),
                LikeCount = request.AddToLikes > likesLimit ? likesLimit : request.AddToLikes
            };
            
            await DatabaseContext.ArticleLikes.AddAsync(entity, cancellationToken);
        }

        private static void UpdateCurrentArticleLikes(bool isAnonymousUser, Domain.Entities.ArticleLikes entity, int likesToBeAdded)
        {
            var likesLimit = isAnonymousUser 
                ? Constants.Likes.LikesLimitForAnonymous 
                : Constants.Likes.LikesLimitForUser;
            
            var sum = entity.LikeCount + likesToBeAdded;
            entity.LikeCount = sum > likesLimit ? likesLimit : sum;
        }
    }
}