using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Persistence.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using TokanPages.Services.AzureStorageService.Factory;

namespace TokanPages.Backend.Application.Content.Queries;

public class GetContentManifestQueryHandler : RequestHandler<GetContentManifestQuery, GetContentManifestQueryResult>
{
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IJsonSerializer _jsonSerializer;

    public GetContentManifestQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureBlobStorageFactory azureBlobStorageFactory, IJsonSerializer jsonSerializer) : base(databaseContext, loggerService)
    {
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _jsonSerializer = jsonSerializer;
    }

    public override async Task<GetContentManifestQueryResult> Handle(GetContentManifestQuery _, CancellationToken cancellationToken)
    {
        const string requestUrl = "content/components/__manifest.json";
        var azureBlob = _azureBlobStorageFactory.Create();
        var contentStream = await azureBlob.OpenRead(requestUrl, cancellationToken);

        if (contentStream is null)
            throw new BusinessException(nameof(ErrorCodes.ASSET_NOT_FOUND), ErrorCodes.ASSET_NOT_FOUND);

        if (contentStream.Content is null)
            throw new BusinessException(nameof(ErrorCodes.ASSET_NOT_FOUND), ErrorCodes.ASSET_NOT_FOUND);

        if (contentStream.ContentType is null)
            throw new BusinessException(nameof(ErrorCodes.ASSET_CONTENT_TYPE_MISSING), ErrorCodes.ASSET_CONTENT_TYPE_MISSING);

        var memoryStream = new MemoryStream();
        await contentStream.Content.CopyToAsync(memoryStream, cancellationToken);
 
        var bytes = memoryStream.ToArray();
        var strings = Encoding.UTF8.GetString(bytes);
 
        return _jsonSerializer.Deserialize<GetContentManifestQueryResult>(strings);
    }
}