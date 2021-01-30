using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.Dto.Users;

namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{
    public class GetArticleQueryHandler : TemplateHandler<GetArticleQuery, GetArticleQueryResult>
    {
        private readonly DatabaseContext FDatabaseContext;

        public GetArticleQueryHandler(DatabaseContext ADatabaseContext)
        {
            FDatabaseContext = ADatabaseContext;
        }

        public override async Task<GetArticleQueryResult> Handle(GetArticleQuery ARequest, CancellationToken ACancellationToken)
        {
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
                    Author = new GetUserDto 
                    { 
                        AliasName = Fields.User.UserAlias,
                        AvatarName = "", // TODO: add new column with current user avatar name
                        FirstName = Fields.User.FirstName,
                        LastName = Fields.User.LastName,
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
