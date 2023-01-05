using System.Text;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Abstractions;

namespace TokanPages.Backend.Application.Content.Queries;

public class GetContentQueryHandler : RequestHandler<GetContentQuery, GetContentQueryResult>
{
    private const string DefaultLanguage = "eng";
        
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IJsonSerializer _jsonSerializer;

    public GetContentQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IJsonSerializer jsonSerializer, IAzureBlobStorageFactory azureBlobStorageFactory) : base(databaseContext, loggerService)
    {
        _jsonSerializer = jsonSerializer;
        _azureBlobStorageFactory = azureBlobStorageFactory;
    }

    public override async Task<GetContentQueryResult> Handle(GetContentQuery request, CancellationToken cancellationToken)
    {
        var selectedLanguage = string.IsNullOrEmpty(request.Language) 
            ? DefaultLanguage
            : request.Language;

        if (request.Type is not ("component" or "document"))
            throw new BusinessException(nameof(ErrorCodes.COMPONENT_TYPE_NOT_SUPPORTED), ErrorCodes.COMPONENT_TYPE_NOT_SUPPORTED);
        
        var componentRequestUrl = $"content/{request.Type}s/{request.Name}.json";
        var azureBob = _azureBlobStorageFactory.Create();
        var contentStream = await azureBob.OpenRead(componentRequestUrl, cancellationToken);

        if (contentStream is null)
            throw new BusinessException(nameof(ErrorCodes.COMPONENT_NOT_FOUND), ErrorCodes.COMPONENT_NOT_FOUND);

        if (contentStream.Content is null)
            throw new BusinessException(nameof(ErrorCodes.COMPONENT_NOT_FOUND), ErrorCodes.COMPONENT_NOT_FOUND);

        var memoryStream = new MemoryStream();
        await contentStream.Content.CopyToAsync(memoryStream, cancellationToken);
        var componentContent = Encoding.Default.GetString(memoryStream.ToArray());

        if (string.IsNullOrEmpty(componentContent))
            throw new BusinessException(nameof(ErrorCodes.COMPONENT_CONTENT_EMPTY), ErrorCodes.COMPONENT_CONTENT_EMPTY);

        var parsed = _jsonSerializer.Parse(componentContent);
        var content = parsed.SelectToken(selectedLanguage);

        if (content == null)
            throw new BusinessException(nameof(ErrorCodes.COMPONENT_CONTENT_MISSING_TOKEN), ErrorCodes.COMPONENT_CONTENT_MISSING_TOKEN);

        return new GetContentQueryResult
        {
            ContentType = request.Type,
            ContentName = request.Name,
            Content = content
        };
    }
}