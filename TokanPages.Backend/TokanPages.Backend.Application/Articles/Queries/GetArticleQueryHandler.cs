using System.Text;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TokanPages.Backend.Application.Articles.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticleQueryHandler : RequestHandler<GetArticleQuery, GetArticleQueryResult>
{
    private readonly IUserService _userService;

    private readonly IJsonSerializer _jsonSerializer;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;
        
    public GetArticleQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, IUserService userService, 
        IJsonSerializer jsonSerializer, IAzureBlobStorageFactory azureBlobStorageFactory) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _jsonSerializer = jsonSerializer;
        _azureBlobStorageFactory = azureBlobStorageFactory;
    }

    public override async Task<GetArticleQueryResult> Handle(GetArticleQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUser(cancellationToken);
        var isAnonymousUser = user == null;

        var requestId = Guid.Empty;
        if (request.Id is not null)
            requestId = (Guid)request.Id;

        if (!string.IsNullOrWhiteSpace(request.Title))
            requestId = await GetArticleIdByTitle(request.Title, cancellationToken);

        var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        var textAsString = await GetArticleTextContent(requestId, cancellationToken);
        var textAsObject = _jsonSerializer.Deserialize<List<ArticleSectionDto>>(textAsString, settings);

        var userLikes = await DatabaseContext.ArticleLikes
            .AsNoTracking()
            .Where(likes => likes.ArticleId == requestId)
            .WhereIfElse(isAnonymousUser,
                likes => likes.IpAddress == _userService.GetRequestIpAddress(),
                likes => likes.UserId == user!.UserId)
            .Select(likes => likes.LikeCount)
            .SumAsync(cancellationToken);

        var totalLikes = await DatabaseContext.ArticleLikes
            .AsNoTracking()
            .Where(likes => likes.ArticleId == requestId)
            .Select(likes => likes.LikeCount)
            .SumAsync(cancellationToken);

        var query = await (from articles in DatabaseContext.Articles
            join userInfo in DatabaseContext.UserInfo
            on articles.UserId equals userInfo.UserId
            join users in DatabaseContext.Users
            on articles.UserId equals users.Id
            where articles.Id == requestId
            select new GetArticleQueryResult
            {
                Id = articles.Id,
                Title = articles.Title,
                Description = articles.Description,
                IsPublished = articles.IsPublished,
                CreatedAt = articles.CreatedAt,
                UpdatedAt = articles.UpdatedAt,
                ReadCount = articles.ReadCount,
                LanguageIso = articles.LanguageIso,
                LikeCount = totalLikes,
                UserLikes = userLikes,
                Author = new GetUserDto
                {
                    UserId = users.Id,
                    AliasName = users.UserAlias,
                    AvatarName = userInfo.UserImageName,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    ShortBio = userInfo.UserAboutText,
                    Registered = userInfo.CreatedAt
                },
                Text = textAsObject
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        if (query is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        return query;
    }

    private async Task<Guid> GetArticleIdByTitle(string title, CancellationToken cancellationToken = default)
    {
        var comparableTitle = title.Replace("-", " ").ToLower();
        return await DatabaseContext.Articles
            .AsNoTracking()
            .Where(articles => articles.Title.ToLower() == comparableTitle)
            .Select(articles => articles.Id)
            .SingleOrDefaultAsync(cancellationToken);
    }

    private async Task<string> GetArticleTextContent(Guid articleId, CancellationToken cancellationToken)
    {
        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        var contentStream = await azureBlob.OpenRead($"content/articles/{articleId}/text.json", cancellationToken);

        if (contentStream is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        if (contentStream.Content is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
        
        var memoryStream = new MemoryStream();
        await contentStream.Content.CopyToAsync(memoryStream, cancellationToken);

        return Encoding.Default.GetString(memoryStream.ToArray());
    }
}