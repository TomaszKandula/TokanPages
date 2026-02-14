using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TokanPages.Backend.Application.Content.Components.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Content;
using TokanPages.Services.AzureStorageService.Abstractions;

namespace TokanPages.Backend.Application.Content.Components.Queries;

public class GetContentManifestQueryHandler : RequestHandler<GetContentManifestQuery, GetContentManifestQueryResult>
{
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IJsonSerializer _jsonSerializer;

    private readonly IContentRepository _contentRepository;

    public GetContentManifestQueryHandler(ILoggerService loggerService, IAzureBlobStorageFactory azureBlobStorageFactory, 
        IJsonSerializer jsonSerializer, IContentRepository contentRepository) : base(loggerService)
    {
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _jsonSerializer = jsonSerializer;
        _contentRepository = contentRepository;
    }

    public override async Task<GetContentManifestQueryResult> Handle(GetContentManifestQuery request, CancellationToken cancellationToken)
    {
        const string requestUrl = "content/data/__manifest.json";
        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        var contentStream = await azureBlob.OpenRead(requestUrl, cancellationToken);

        if (contentStream?.Content is null)
            throw new BusinessException(nameof(ErrorCodes.ASSET_NOT_FOUND), ErrorCodes.ASSET_NOT_FOUND);

        if (contentStream.ContentType is null)
            throw new BusinessException(nameof(ErrorCodes.ASSET_CONTENT_TYPE_MISSING), ErrorCodes.ASSET_CONTENT_TYPE_MISSING);

        var memoryStream = new MemoryStream();
        await contentStream.Content.CopyToAsync(memoryStream, cancellationToken);
 
        var bytes = memoryStream.ToArray();
        var strings = Encoding.UTF8.GetString(bytes);

        var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        var manifest = _jsonSerializer.Deserialize<GetContentManifestQueryResult>(strings, settings);

        var languageList = await _contentRepository.GetContentLanguageList();
        if (languageList is null)
            return manifest;

        var languages = languageList
            .Select(item => new LanguageModel
            {
                Id = item.Id,
                Name = item.Name,
                Iso =  item.Iso,
                IsDefault =  item.IsDefault,
            })
            .ToList();

        manifest.Languages = languages;
        return manifest;
    }
}