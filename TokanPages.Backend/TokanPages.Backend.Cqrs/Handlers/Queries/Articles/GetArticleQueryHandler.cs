namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles
{
    using System.Net;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Utilities.LoggerService;
    using Storage.Models;
    using Core.Exceptions;
    using Core.Extensions;
    using Shared.Resources;
    using Shared.Dto.Users;
    using Shared.Dto.Content.Common;
    using Services.UserServiceProvider;
    using Core.Utilities.JsonSerializer;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;

    public class GetArticleQueryHandler : TemplateHandler<GetArticleQuery, GetArticleQueryResult>
    {
        private readonly DatabaseContext _databaseContext;

        private readonly IUserServiceProvider _userServiceProvider;

        private readonly IJsonSerializer _jsonSerializer;

        private readonly AzureStorage _azureStorage;

        private readonly ICustomHttpClient _customHttpClient;
        
        public GetArticleQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, IUserServiceProvider userServiceProvider, 
            IJsonSerializer jsonSerializer, AzureStorage azureStorage, ICustomHttpClient customHttpClient) : base(databaseContext, loggerService)
        {
            _databaseContext = databaseContext;
            _userServiceProvider = userServiceProvider;
            _jsonSerializer = jsonSerializer;
            _azureStorage = azureStorage;
            _customHttpClient = customHttpClient;
        }

        public override async Task<GetArticleQueryResult> Handle(GetArticleQuery request, CancellationToken cancellationToken)
        {
            var userId = await _userServiceProvider.GetUserId();
            var isAnonymousUser = userId == null;

            var textRequestUrl = $"{_azureStorage.BaseUrl}/content/articles/{request.Id}/text.json";
            var textAsString = await GetJsonData(textRequestUrl, cancellationToken);
            var textAsObject = _jsonSerializer.Deserialize<List<Section>>(textAsString);

            var getArticleLikes = await _databaseContext.ArticleLikes
                .Where(likes => likes.ArticleId == request.Id)
                .WhereIfElse(isAnonymousUser,
                    likes => likes.IpAddress == _userServiceProvider.GetRequestIpAddress(),
                    likes => likes.UserId == userId)
                .Select(likes => likes.LikeCount)
                .ToListAsync(cancellationToken);

            var articleLikes = !getArticleLikes.Any() ? 0 : getArticleLikes.FirstOrDefault();
            var currentArticle = await _databaseContext.Articles
                .AsNoTracking()
                .Include(articles => articles.ArticleLikes)
                .Include(articles => articles.User)
                .Where(articles => articles.Id == request.Id)
                .Select(articles => new GetArticleQueryResult
                {
                    Id = articles.Id,
                    Title = articles.Title,
                    Description = articles.Description,
                    IsPublished = articles.IsPublished,
                    CreatedAt = articles.CreatedAt,
                    UpdatedAt = articles.UpdatedAt,
                    ReadCount = articles.ReadCount,
                    LikeCount = articles.ArticleLikes
                        .Where(likes => likes.ArticleId == request.Id)
                        .Select(likes => likes.LikeCount)
                        .Sum(),
                    UserLikes = articleLikes,
                    Author = new GetUserDto
                    {
                        AliasName = articles.User.UserAlias,
                        AvatarName = articles.User.AvatarName,
                        FirstName = articles.User.FirstName,
                        LastName = articles.User.LastName,
                        ShortBio = articles.User.ShortBio,
                        Registered = articles.User.Registered
                    },
                    Text = textAsObject
                })
                .ToListAsync(cancellationToken);

            if (!currentArticle.Any())
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

            return currentArticle.First();
        }

        private async Task<string> GetJsonData(string url, CancellationToken cancellationToken)
        {
            var configuration = new Configuration { Url = url, Method = "GET" };
            var results = await _customHttpClient.Execute(configuration, cancellationToken);

            if (results.StatusCode == HttpStatusCode.NotFound)
                throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
            
            if (results.StatusCode != HttpStatusCode.OK)
                throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

            return results.Content == null 
                ? string.Empty 
                : System.Text.Encoding.Default.GetString(results.Content);
        }
    }
}