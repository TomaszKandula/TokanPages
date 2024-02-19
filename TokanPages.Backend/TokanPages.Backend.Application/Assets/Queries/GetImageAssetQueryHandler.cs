using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Services.AzureStorageService.Abstractions;

namespace TokanPages.Backend.Application.Assets.Queries;

public class GetImageAssetQueryHandler : RequestHandler<GetImageAssetQuery, FileContentResult>
{
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public GetImageAssetQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureBlobStorageFactory azureBlobStorageFactory) : base(databaseContext, loggerService) => _azureBlobStorageFactory = azureBlobStorageFactory;

    public override async Task<FileContentResult> Handle(GetImageAssetQuery request, CancellationToken cancellationToken)
    {
        var requestUrl = $"content/assets/{request.BlobName}";
        var azureBlob = _azureBlobStorageFactory.Create();

        var streamContent = await azureBlob.OpenRead(requestUrl, cancellationToken);
        if (streamContent is null)
            throw new BusinessException(nameof(ErrorCodes.ASSET_NOT_FOUND), ErrorCodes.ASSET_NOT_FOUND);

        var memoryStream = new MemoryStream();

        if (streamContent.Content is null)
            throw new BusinessException(nameof(ErrorCodes.ASSET_NOT_FOUND), ErrorCodes.ASSET_NOT_FOUND);

        if (streamContent.ContentType is null)
            throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

        await streamContent.Content.CopyToAsync(memoryStream, cancellationToken);
        return new FileContentResult(memoryStream.ToArray(), streamContent.ContentType);
    }
}