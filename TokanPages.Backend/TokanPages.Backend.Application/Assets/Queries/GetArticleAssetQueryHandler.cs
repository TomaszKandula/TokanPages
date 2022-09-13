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

public class GetArticleAssetQueryHandler : RequestHandler<GetArticleAssetQuery, FileContentResult>
{
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;
    
    public GetArticleAssetQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, IAzureBlobStorageFactory azureBlobStorageFactory) 
        : base(databaseContext, loggerService) => _azureBlobStorageFactory = azureBlobStorageFactory;

    public override async Task<FileContentResult> Handle(GetArticleAssetQuery request, CancellationToken cancellationToken)
    {
        var requestUrl = $"content/articles/{request.Id}/{request.AssetName}";
        var azureBlob = _azureBlobStorageFactory.Create();

        var streamContent = await azureBlob.OpenRead(requestUrl, cancellationToken);
        if (streamContent is null)
            throw new BusinessException(nameof(ErrorCodes.ASSET_NOT_FOUND), ErrorCodes.ASSET_NOT_FOUND);

        if (streamContent.Content is null)
            throw new BusinessException(nameof(ErrorCodes.ASSET_NOT_FOUND), ErrorCodes.ASSET_NOT_FOUND);

        if (streamContent.ContentType is null)
            throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

        var memoryStream = new MemoryStream();
        await streamContent.Content.CopyToAsync(memoryStream, cancellationToken);
        return new FileContentResult(memoryStream.ToArray(), streamContent.ContentType);
    }
}