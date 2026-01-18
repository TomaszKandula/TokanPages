using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Queries;

public class GetArticleQueryHandler : RequestHandler<GetArticleQuery, GetArticleQueryResult>
{
    private readonly IUserService _userService;

    private readonly IJsonSerializer _jsonSerializer;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IArticlesRepository _articlesRepository;

    public GetArticleQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IUserService userService, 
        IJsonSerializer jsonSerializer, IAzureBlobStorageFactory azureBlobStorageFactory, IArticlesRepository articlesRepository) 
        : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _jsonSerializer = jsonSerializer;
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _articlesRepository = articlesRepository;
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
            requestId = await _articlesRepository.GetArticleIdByTitle(request.Title, cancellationToken);

        var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        var textAsString = await GetArticleTextContent(requestId, cancellationToken);
        var textAsObject = _jsonSerializer.Deserialize<List<ArticleSectionDto>>(textAsString, settings);

        var output = await _articlesRepository.GetArticle(userId, requestId, isAnonymousUser, ipAddress, userLanguage, cancellationToken);

        return new GetArticleQueryResult
        {
            Id = output.Id,
            Title = output.Title,
            CategoryName = output.CategoryName,
            Description = output.Description,
            IsPublished = output.IsPublished,
            CreatedAt = output.CreatedAt,
            UpdatedAt = output.UpdatedAt,
            ReadCount = output.ReadCount,
            LanguageIso = output.LanguageIso,
            TotalLikes = output.TotalLikes,
            UserLikes = output.UserLikes,
            Author = output.Author,
            Tags = output.Tags,
            Text = textAsObject
        };
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