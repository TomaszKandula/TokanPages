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
using Dto.Users;
using Services.UserService;
using Dto.Content.Common;
using Core.Utilities.LoggerService;
using Core.Utilities.JsonSerializer;
using Services.AzureStorageService.Factory;

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

        var textAsString = await GetArticleTextContent(request.Id, cancellationToken);
        var textAsObject = _jsonSerializer.Deserialize<List<Section>>(textAsString);

        var userLikes = await DatabaseContext.ArticleLikes
            .AsNoTracking()
            .Where(likes => likes.ArticleId == request.Id)
            .WhereIfElse(isAnonymousUser,
                likes => likes.IpAddress == _userService.GetRequestIpAddress(),
                likes => likes.UserId == user!.UserId)
            .Select(likes => likes.LikeCount)
            .SumAsync(cancellationToken);

        var totalLikes = await DatabaseContext.ArticleLikes
            .AsNoTracking()
            .Where(likes => likes.ArticleId == request.Id)
            .Select(likes => likes.LikeCount)
            .SumAsync(cancellationToken);

        var query = await (from articles in DatabaseContext.Articles
            join userInfo in DatabaseContext.UserInfo
            on articles.UserId equals userInfo.UserId
            join users in DatabaseContext.Users
            on articles.UserId equals users.Id
            where articles.Id == request.Id
            select new GetArticleQueryResult
            {
                Id = articles.Id,
                Title = articles.Title,
                Description = articles.Description,
                IsPublished = articles.IsPublished,
                CreatedAt = articles.CreatedAt,
                UpdatedAt = articles.UpdatedAt,
                ReadCount = articles.ReadCount,
                LikeCount = totalLikes,
                UserLikes = userLikes,
                Author = new GetUserDto
                {
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

    private async Task<string> GetArticleTextContent(Guid articleId, CancellationToken cancellationToken)
    {
        var azureBlob = _azureBlobStorageFactory.Create();
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