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
using TokanPages.Persistence.Database.Contexts;
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
        var userId = _userService.GetLoggedUserId();
        var userLanguage = _userService.GetRequestUserLanguage();
        var ipAddress = _userService.GetRequestIpAddress();
        var isAnonymousUser = userId == Guid.Empty;

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
            .Where(like => like.ArticleId == requestId)
            .WhereIfElse(isAnonymousUser, 
                like => like.IpAddress == ipAddress && like.UserId == null, 
                like => like.UserId == userId)
            .Select(like => like.LikeCount)
            .SumAsync(cancellationToken);

        var totalLikes = await DatabaseContext.ArticleLikes
            .AsNoTracking()
            .Where(like => like.ArticleId == requestId)
            .Select(like => like.LikeCount)
            .SumAsync(cancellationToken);

        var articleData = await (from article in DatabaseContext.Articles
            join articleCategory in DatabaseContext.ArticleCategories 
                on article.CategoryId equals articleCategory.Id
            join categoryName in DatabaseContext.CategoryNames
                on articleCategory.Id equals categoryName.ArticleCategoryId
            join language in DatabaseContext.Languages
                on categoryName.LanguageId equals language.Id
            where language.LangId == userLanguage
            where article.Id == requestId
            select new 
            {
                article.Id,
                article.UserId,
                article.Title,
                article.Description,
                article.IsPublished,
                article.CreatedAt,
                article.UpdatedAt,
                article.ReadCount,
                article.LanguageIso,
                categoryName.Name,
                TotalLikes = totalLikes,
                UserLikes = userLikes,
                Text = textAsObject
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(cancellationToken);

        if (articleData is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        var userDto = await (from user in DatabaseContext.Users
            join userInfo in DatabaseContext.UserInformation 
            on user.Id equals userInfo.UserId
            where user.Id == articleData.UserId
            select new GetUserDto
            {
                UserId = user.Id,
                AliasName = user.UserAlias,
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
            Id = articleData.Id,
            Title = articleData.Title,
            CategoryName = articleData.Name,
            Description = articleData.Description,
            IsPublished = articleData.IsPublished,
            CreatedAt = articleData.CreatedAt,
            UpdatedAt = articleData.UpdatedAt,
            ReadCount = articleData.ReadCount,
            LanguageIso = articleData.LanguageIso,
            TotalLikes = totalLikes,
            UserLikes = userLikes,
            Author = userDto,
            Tags = tags,
            Text = textAsObject
        };
    }

    private async Task<Guid> GetArticleIdByTitle(string title, CancellationToken cancellationToken = default)
    {
        var comparableTitle = title.Replace("-", " ").ToLower();
        return await DatabaseContext.Articles
            .AsNoTracking()
            .Where(article => article.Title.ToLower() == comparableTitle)
            .Select(article => article.Id)
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