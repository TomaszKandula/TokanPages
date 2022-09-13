using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Factory;

namespace TokanPages.Backend.Application.Assets.Commands;

public class AddSingleAssetCommandHandler : RequestHandler<AddSingleAssetCommand, AddSingleAssetCommandResult>
{
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public AddSingleAssetCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, IAzureBlobStorageFactory azureBlobStorageFactory) 
        : base(databaseContext, loggerService) => _azureBlobStorageFactory = azureBlobStorageFactory;

    public override async Task<AddSingleAssetCommandResult> Handle(AddSingleAssetCommand request, CancellationToken cancellationToken)
    {
        var azureBlob = _azureBlobStorageFactory.Create();
        var stream = new MemoryStream(request.Data);

        var mediaName = $"{Guid.NewGuid():N}_{request.MediaName}".ToUpper();
        var blobName = $"{request.MediaType}/{mediaName}";
        var destinationPath = $"content/assets/{blobName}";

        await azureBlob.UploadFile(stream, destinationPath, cancellationToken: cancellationToken);
        LoggerService.LogInformation($"New asset has been saved. Path: {destinationPath}.");

        return new AddSingleAssetCommandResult { BlobName  = blobName };
    }
}