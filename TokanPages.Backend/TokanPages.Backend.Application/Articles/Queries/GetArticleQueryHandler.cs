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
        var userLanguage = _userService.GetRequestUserLanguage();
        var ipAddress = _userService.GetRequestIpAddress();
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
                likes => likes.IpAddress == ipAddress && likes.UserId == null, 
                likes => likes.UserId == user!.UserId)
            .Select(likes => likes.LikeCount)
            .SumAsync(cancellationToken);

        var totalLikes = await DatabaseContext.ArticleLikes
            .AsNoTracking()
            .Where(likes => likes.ArticleId == requestId)
            .Select(likes => likes.LikeCount)
            .SumAsync(cancellationToken);

        var article = await (from articles in DatabaseContext.Articles
            join articleCategory in DatabaseContext.ArticleCategories 
                on articles.CategoryId equals articleCategory.Id
            join categoryNames in DatabaseContext.CategoryNames
                on articleCategory.Id equals categoryNames.ArticleCategoryId
            join languages in DatabaseContext.Languages
                on categoryNames.LanguageId equals languages.Id
            where languages.LangId == userLanguage
            where articles.Id == requestId
            select new 
            {
                articles.Id,
                articles.UserId,
                articles.Title,
                articles.Description,
                articles.IsPublished,
                articles.CreatedAt,
                articles.UpdatedAt,
                articles.ReadCount,
                articles.LanguageIso,
                categoryNames.Name,
                TotalLikes = totalLikes,
                UserLikes = userLikes,
                Text = textAsObject
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        if (article is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        var author = await (from users in DatabaseContext.Users
            join userInfo in DatabaseContext.UserInformation 
            on users.Id equals userInfo.UserId
            where users.Id == article.UserId
            select new GetUserDto
            {
                UserId = users.Id,
                AliasName = users.UserAlias,
                AvatarName = userInfo.UserImageName,
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                ShortBio = userInfo.UserAboutText,
                Registered = userInfo.CreatedAt
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        var tags = await (from articleTags in DatabaseContext.ArticleTags
            join articles in DatabaseContext.Articles
            on articleTags.ArticleId equals articles.Id
            where articles.Id == requestId
            select articleTags.TagName)
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

        return new GetArticleQueryResult
        {
            Id = article.Id,
            Title = article.Title,
            CategoryName = article.Name,
            Description = article.Description,
            IsPublished = article.IsPublished,
            CreatedAt = article.CreatedAt,
            UpdatedAt = article.UpdatedAt,
            ReadCount = article.ReadCount,
            LanguageIso = article.LanguageIso,
            TotalLikes = totalLikes,
            UserLikes = userLikes,
            Author = author,
            Tags = tags,
            Text = textAsObject
        };
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

    private async Task<string> GetArticleTextContent(Guid articleId, CancellationToken cancellationToken = default)
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