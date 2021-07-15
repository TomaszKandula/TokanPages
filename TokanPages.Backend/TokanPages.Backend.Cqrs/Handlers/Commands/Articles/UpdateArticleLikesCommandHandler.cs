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
    using Services.UserServiceProvider;
    using MediatR;

    public class UpdateArticleLikesCommandHandler : TemplateHandler<UpdateArticleLikesCommand, Unit>
    {
        private readonly DatabaseContext FDatabaseContext;

        private readonly IUserServiceProvider FUserServiceProvider;
        
        public UpdateArticleLikesCommandHandler(DatabaseContext ADatabaseContext, IUserServiceProvider AUserServiceProvider)
        {
            FDatabaseContext = ADatabaseContext;
            FUserServiceProvider = AUserServiceProvider;
        }

        public override async Task<Unit> Handle(UpdateArticleLikesCommand ARequest, CancellationToken ACancellationToken)
        {
            var LArticles = await FDatabaseContext.Articles
                .Where(AArticles => AArticles.Id == ARequest.Id)
                .ToListAsync(ACancellationToken);

            if (!LArticles.Any())
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

            var LUserId = await FUserServiceProvider.GetUserId();
            var LIsAnonymousUser = LUserId == null;
            
            var LArticleLikes = await FDatabaseContext.ArticleLikes
                .Where(ALikes => ALikes.ArticleId == ARequest.Id)
                .WhereIfElse(LIsAnonymousUser,
                    ALikes => ALikes.IpAddress == FUserServiceProvider.GetRequestIpAddress(),
                    ALikes => ALikes.UserId == LUserId)
                .ToListAsync(ACancellationToken);

            if (!LArticleLikes.Any())
            {
                await AddNewArticleLikes(LIsAnonymousUser, ARequest, ACancellationToken);
            }
            else
            {
                UpdateCurrentArticleLikes(LIsAnonymousUser, LArticleLikes.First(), ARequest.AddToLikes);
            }

            await FDatabaseContext.SaveChangesAsync(ACancellationToken);
            return await Task.FromResult(Unit.Value);
        }
        
        private async Task AddNewArticleLikes(bool AIsAnonymousUser, UpdateArticleLikesCommand ARequest, CancellationToken ACancellationToken)
        {
            var LLikesLimit = AIsAnonymousUser 
                ? Constants.Likes.LIKES_LIMIT_FOR_ANONYMOUS 
                : Constants.Likes.LIKES_LIMIT_FOR_USER;

            var LEntity = new Domain.Entities.ArticleLikes
            {
                ArticleId = ARequest.Id,
                UserId = await FUserServiceProvider.GetUserId(),
                IpAddress = FUserServiceProvider.GetRequestIpAddress(),
                LikeCount = ARequest.AddToLikes > LLikesLimit ? LLikesLimit : ARequest.AddToLikes
            };
            
            await FDatabaseContext.ArticleLikes.AddAsync(LEntity, ACancellationToken);
        }

        private static void UpdateCurrentArticleLikes(bool AIsAnonymousUser, Domain.Entities.ArticleLikes AEntity, int ALikesToBeAdded)
        {
            var LLikesLimit = AIsAnonymousUser 
                ? Constants.Likes.LIKES_LIMIT_FOR_ANONYMOUS 
                : Constants.Likes.LIKES_LIMIT_FOR_USER;
            
            var LLikesSum = AEntity.LikeCount + ALikesToBeAdded;
            AEntity.LikeCount = LLikesSum > LLikesLimit ? LLikesLimit : LLikesSum;
        }
    }
}