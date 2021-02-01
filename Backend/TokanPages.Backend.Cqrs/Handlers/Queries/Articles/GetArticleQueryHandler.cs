using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.Dto.Users;
using TokanPages.Backend.Cqrs.Services.UserProvider;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{
    public class GetArticleQueryHandler : TemplateHandler<GetArticleQuery, GetArticleQueryResult>
    {
        private readonly DatabaseContext FDatabaseContext;
        private readonly IUserProvider FUserProvider;

        public GetArticleQueryHandler(DatabaseContext ADatabaseContext, IUserProvider AUserProvider)
        {
            FDatabaseContext = ADatabaseContext;
            FUserProvider = AUserProvider;
        }

        public override async Task<GetArticleQueryResult> Handle(GetArticleQuery ARequest, CancellationToken ACancellationToken)
        {
            var IsAnonymousUser = FUserProvider.GetUserId() == null;

            var LGetArticleLikes = await FDatabaseContext.Likes
                .Where(Likes => Likes.ArticleId == ARequest.Id)
                .WhereIfElse(IsAnonymousUser,
                    Likes => Likes.IpAddress == FUserProvider.GetRequestIpAddress(),
                    Likes => Likes.UserId == FUserProvider.GetUserId())
                .Select(ALikes => ALikes.LikeCount)
                .ToListAsync(ACancellationToken);

            var LArticleLikes = !LGetArticleLikes.Any() ? 0 : LGetArticleLikes.FirstOrDefault();

            var LCurrentArticle = await FDatabaseContext.Articles
                .AsNoTracking()
                .Include(ALikes => ALikes.Likes)
                .Include(AUsers => AUsers.User)
                .Where(AArticles => AArticles.Id == ARequest.Id)
                .Select(Fields => new GetArticleQueryResult
                {
                    Id = Fields.Id,
                    Title = Fields.Title,
                    Description = Fields.Description,
                    IsPublished = Fields.IsPublished,
                    CreatedAt = Fields.CreatedAt,
                    UpdatedAt = Fields.UpdatedAt,
                    ReadCount = Fields.ReadCount,
                    LikeCount = Fields.Likes
                        .Where(ALikes => ALikes.ArticleId == ARequest.Id)
                        .Select(ALikes => ALikes.LikeCount)
                        .Sum(),
                    UserLikes = LArticleLikes,
                    Author = new GetUserDto
                    {
                        AliasName = Fields.User.UserAlias,
                        AvatarName = Fields.User.AvatarName,
                        FirstName = Fields.User.FirstName,
                        LastName = Fields.User.LastName,
                        ShortBio = Fields.User.ShortBio,
                        Registered = Fields.User.Registered
                    }
                })
                .ToListAsync(ACancellationToken);

            if (!LCurrentArticle.Any())
            {
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
            }

            return LCurrentArticle.First();
        }
    }
}
