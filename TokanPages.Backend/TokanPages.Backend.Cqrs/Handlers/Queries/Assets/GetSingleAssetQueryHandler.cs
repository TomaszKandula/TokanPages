namespace TokanPages.Backend.Cqrs.Handlers.Queries.Assets;

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Database;
using Core.Exceptions;
using Shared.Resources;
using Core.Utilities.LoggerService;
using Services.AzureStorageService.Factory;

public class GetSingleAssetQueryHandler : RequestHandler<GetSingleAssetQuery, FileContentResult>
{
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public GetSingleAssetQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureBlobStorageFactory azureBlobStorageFactory) : base(databaseContext, loggerService)
    {
        _azureBlobStorageFactory = azureBlobStorageFactory;
    }

    public override async Task<FileContentResult> Handle(GetSingleAssetQuery request, CancellationToken cancellationToken)
    {
        var requestUrl = $"content/assets/{request.BlobName}";
        var azureBlob = _azureBlobStorageFactory.Create();

        var streamContent = await azureBlob.OpenRead(requestUrl, cancellationToken);
        if (streamContent is null)
            throw new BusinessException(nameof(ErrorCodes.ASSET_NOT_FOUND), ErrorCodes.ASSET_NOT_FOUND);
        
        var memoryStream = new MemoryStream();
        await streamContent.Content.CopyToAsync(memoryStream, cancellationToken);
        return new FileContentResult(memoryStream.ToArray(), streamContent.ContentType);
    }
}