namespace TokanPages.Backend.Cqrs.Handlers.Queries.Articles;

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Database;
using Core.Exceptions;
using Core.Extensions;
using Shared.Resources;
using Shared.Dto.Users;
using Services.UserService;
using Shared.Dto.Content.Common;
using Core.Utilities.LoggerService;
using Core.Utilities.JsonSerializer;
using Services.AzureStorageService.Factory;

public class GetArticleQueryHandler : RequestHandler<GetArticleQuery, GetArticleQueryResult>
{
    private readonly DatabaseContext _databaseContext;

    private readonly IUserService _userService;

    private readonly IJsonSerializer _jsonSerializer;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;
        
    public GetArticleQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, IUserService userService, 
        IJsonSerializer jsonSerializer, IAzureBlobStorageFactory azureBlobStorageFactory) : base(databaseContext, loggerService)
    {
        _databaseContext = databaseContext;
        _userService = userService;
        _jsonSerializer = jsonSerializer;
        _azureBlobStorageFactory = azureBlobStorageFactory;
    }

    public override async Task<GetArticleQueryResult> Handle(GetArticleQuery request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserId();
        var isAnonymousUser = userId == null;

        var textAsString = await GetArticleTextContent(request.Id, cancellationToken);
        var textAsObject = _jsonSerializer.Deserialize<List<Section>>(textAsString);
        
        var getArticleLikes = await _databaseContext.ArticleLikes
            .Where(likes => likes.ArticleId == request.Id)
            .WhereIfElse(isAnonymousUser,
                likes => likes.IpAddress == _userService.GetRequestIpAddress(),
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

    private async Task<string> GetArticleTextContent(Guid articleId, CancellationToken cancellationToken)
    {
        var azureBlob = _azureBlobStorageFactory.Create();
        var streamContent = await azureBlob.OpenRead($"content/articles/{articleId}/text.json", cancellationToken);
        if (streamContent is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
        
        var memoryStream = new MemoryStream();
        await streamContent.Content.CopyToAsync(memoryStream, cancellationToken);

        return Encoding.Default.GetString(memoryStream.ToArray());
    }
}