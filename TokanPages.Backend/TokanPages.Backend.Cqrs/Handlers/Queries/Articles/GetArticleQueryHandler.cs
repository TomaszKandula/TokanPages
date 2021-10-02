namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{
    using System.Net;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Storage.Models;
    using Core.Exceptions;
    using Core.Extensions;
    using Shared.Resources;
    using Shared.Dto.Users;
    using Shared.Dto.Content.Common;
    using Services.UserServiceProvider;
    using Core.Utilities.JsonSerializer;

    public class GetArticleQueryHandler : TemplateHandler<GetArticleQuery, GetArticleQueryResult>
    {
        private readonly DatabaseContext FDatabaseContext;
        
        private readonly IUserServiceProvider FUserServiceProvider;

        private readonly IJsonSerializer FJsonSerializer; 
        
        private readonly AzureStorage FAzureStorage;
        
        private readonly HttpClient FHttpClient;
        
        public GetArticleQueryHandler(DatabaseContext ADatabaseContext, IUserServiceProvider AUserServiceProvider, 
            IJsonSerializer AJsonSerializer, AzureStorage AAzureStorage, HttpClient AHttpClient)
        {
            FDatabaseContext = ADatabaseContext;
            FUserServiceProvider = AUserServiceProvider;
            FJsonSerializer = AJsonSerializer;
            FAzureStorage = AAzureStorage;
            FHttpClient = AHttpClient;
        }

        public override async Task<GetArticleQueryResult> Handle(GetArticleQuery ARequest, CancellationToken ACancellationToken)
        {
            var LUserId = await FUserServiceProvider.GetUserId();
            var LIsAnonymousUser = LUserId == null;

            var LTextRequestUrl = $"{FAzureStorage.BaseUrl}/content/articles/{ARequest.Id}/text.json";
            var LTextAsString = await GetJsonData(LTextRequestUrl, ACancellationToken);
            var LTextAsObject = FJsonSerializer.Deserialize<List<Section>>(LTextAsString);

            var LGetArticleLikes = await FDatabaseContext.ArticleLikes
                .Where(ALikes => ALikes.ArticleId == ARequest.Id)
                .WhereIfElse(LIsAnonymousUser,
                    ALikes => ALikes.IpAddress == FUserServiceProvider.GetRequestIpAddress(),
                    ALikes => ALikes.UserId == LUserId)
                .Select(ALikes => ALikes.LikeCount)
                .ToListAsync(ACancellationToken);

            var LArticleLikes = !LGetArticleLikes.Any() ? 0 : LGetArticleLikes.FirstOrDefault();
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
                    },
                    Text = LTextAsObject
                })
                .ToListAsync(ACancellationToken);

            if (!LCurrentArticle.Any())
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

            return LCurrentArticle.First();
        }

        private async Task<string> GetJsonData(string AUrl, CancellationToken ACancellationToken)
        {
            var LResponseContent = await FHttpClient.GetAsync(AUrl, ACancellationToken);

            if (LResponseContent.StatusCode != HttpStatusCode.OK)
                throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);
                    
            return await LResponseContent.Content.ReadAsStringAsync(ACancellationToken);
        }
    }
}