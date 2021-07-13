namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Exceptions;
    using Core.Extensions;
    using Shared.Resources;
    using Shared.Dto.Users;
    using Services.UserServiceProvider;

    public class GetArticleQueryHandler : TemplateHandler<GetArticleQuery, GetArticleQueryResult>
    {
        private readonly DatabaseContext FDatabaseContext;
        
        private readonly IUserServiceProvider FUserServiceProvider;

        public GetArticleQueryHandler(DatabaseContext ADatabaseContext, IUserServiceProvider AUserServiceProvider)
        {
            FDatabaseContext = ADatabaseContext;
            FUserServiceProvider = AUserServiceProvider;
        }

        public override async Task<GetArticleQueryResult> Handle(GetArticleQuery ARequest, CancellationToken ACancellationToken)
        {
            var LUserId = await FUserServiceProvider.GetUserId();
            var LIsAnonymousUser = LUserId == null;

            var LGetArticleLikes = await FDatabaseContext.ArticleLikes
                .Where(ALikes => ALikes.ArticleId == ARequest.Id)
                .WhereIfElse(LIsAnonymousUser,
                    ALikes => ALikes.IpAddress == FUserServiceProvider.GetRequestIpAddress(),
                    ALikes => ALikes.UserId == LUserId)
                .Select(ALikes => ALikes.LikeCount)
                .ToListAsync(ACancellationToken);

            var LArticleLikes = !LGetArticleLikes.Any() 
                ? 0 
                : LGetArticleLikes.FirstOrDefault();

            var LCurrentArticle = await FDatabaseContext.Articles
                .AsNoTracking()
                .Include(ALikes => ALikes.ArticleLikes)
                .Include(AUsers => AUsers.User)
                .Where(AArticles => AArticles.Id == ARequest.Id)
                .Select(AFields => new GetArticleQueryResult
                {
                    Id = AFields.Id,
                    Title = AFields.Title,
                    Description = AFields.Description,
                    IsPublished = AFields.IsPublished,
                    CreatedAt = AFields.CreatedAt,
                    UpdatedAt = AFields.UpdatedAt,
                    ReadCount = AFields.ReadCount,
                    LikeCount = AFields.ArticleLikes
                        .Where(ALikes => ALikes.ArticleId == ARequest.Id)
                        .Select(ALikes => ALikes.LikeCount)
                        .Sum(),
                    UserLikes = LArticleLikes,
                    Author = new GetUserDto
                    {
                        AliasName = AFields.User.UserAlias,
                        AvatarName = AFields.User.AvatarName,
                        FirstName = AFields.User.FirstName,
                        LastName = AFields.User.LastName,
                        ShortBio = AFields.User.ShortBio,
                        Registered = AFields.User.Registered
                    }
                })
                .ToListAsync(ACancellationToken);

            if (!LCurrentArticle.Any())
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

            return LCurrentArticle.First();
        }
    }
}