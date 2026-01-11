using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Application.Content.Assets.Queries.Models;
using TokanPages.Services.AzureStorageService.Abstractions;

namespace TokanPages.Backend.Application.Content.Assets.Queries;

public class GetNonVideoAssetQueryHandler : RequestHandler<GetNonVideoAssetQuery, ContentOutput>
{
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public GetNonVideoAssetQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureBlobStorageFactory azureBlobStorageFactory) : base(databaseContext, loggerService) => _azureBlobStorageFactory = azureBlobStorageFactory;

    public override async Task<ContentOutput> Handle(GetNonVideoAssetQuery request, CancellationToken cancellationToken)
    {
        var requestUrl = $"content/assets/{request.BlobName}";
        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);

        var streamContent = await azureBlob.OpenRead(requestUrl, cancellationToken);
        if (streamContent is null)
            throw new BusinessException(nameof(ErrorCodes.ASSET_NOT_FOUND), ErrorCodes.ASSET_NOT_FOUND);

        var memoryStream = new MemoryStream();

        if (streamContent.Content is null)
            throw new BusinessException(nameof(ErrorCodes.ASSET_NOT_FOUND), ErrorCodes.ASSET_NOT_FOUND);

        if (streamContent.ContentType is null || streamContent.ContentType.Contains("video"))
            throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

        await streamContent.Content.CopyToAsync(memoryStream, cancellationToken);

        if (request.CanDownload)
            streamContent.ContentType = "application/octet-stream";

        var data = new FileContentResult(memoryStream.ToArray(), streamContent.ContentType);
        return new ContentOutput
        {
            FileContent = data,
            FileName = Path.GetFileName(requestUrl)
        };
    }
}