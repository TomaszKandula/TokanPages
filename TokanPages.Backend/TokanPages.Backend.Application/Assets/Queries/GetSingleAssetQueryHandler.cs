using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Factory;

namespace TokanPages.Backend.Application.Assets.Queries;

public class GetSingleAssetQueryHandler : RequestHandler<GetSingleAssetQuery, FileContentResult>
{
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public GetSingleAssetQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, IAzureBlobStorageFactory azureBlobStorageFactory) 
        : base(databaseContext, loggerService) => _azureBlobStorageFactory = azureBlobStorageFactory;

    public override async Task<FileContentResult> Handle(GetSingleAssetQuery request, CancellationToken cancellationToken)
    {
        var requestUrl = $"content/assets/{request.BlobName}";
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
        return new FileContentResult(memoryStream.ToArray(), contentStream.ContentType);
    }
}