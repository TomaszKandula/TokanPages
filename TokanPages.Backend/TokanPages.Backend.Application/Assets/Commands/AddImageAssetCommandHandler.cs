using System.Text.RegularExpressions;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Abstractions;

namespace TokanPages.Backend.Application.Assets.Commands;

public class AddImageAssetCommandHandler : RequestHandler<AddImageAssetCommand, AddImageAssetCommandResult>
{
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public AddImageAssetCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureBlobStorageFactory azureBlobStorageFactory) : base(databaseContext, loggerService)
        => _azureBlobStorageFactory = azureBlobStorageFactory;

    public override async Task<AddImageAssetCommandResult> Handle(AddImageAssetCommand request, CancellationToken cancellationToken)
    {
        if (request.Base64Data is not null && request.Base64Data.Length > 0)
            return await ProcessBase64(request, cancellationToken);

        if (request.BinaryData is not null && request.BinaryData.Length > 0)
            return await ProcessBinaryData(request, cancellationToken);

        throw new GeneralException(nameof(ErrorCodes.ASSET_NOT_FOUND), ErrorCodes.ASSET_NOT_FOUND);
    }

    private async Task<AddImageAssetCommandResult> ProcessBase64(AddImageAssetCommand request, CancellationToken cancellationToken)
    {
        var base64Data = request.Base64Data;
        var base64Raw = base64Data!.Split(",")[1];

        var regex = new Regex(@"data:(.*);base64");
        var match = regex.Match(base64Data);

        var contentType = match.Groups[1].Value;
        var extension = contentType.Split("/")[1];
        var binary = Convert.FromBase64String(base64Raw);

        var fileName = $"{Guid.NewGuid():N}.{extension}".ToLower();
        var blobName = $"images/{fileName}";
        var destinationPath = $"content/assets/{blobName}";

        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        using var stream = new MemoryStream(binary);

        await azureBlob.UploadFile(stream, destinationPath, contentType, cancellationToken);
        LoggerService.LogInformation($"New image asset has been saved. Path: {destinationPath}.");

        return new AddImageAssetCommandResult
        {
            BlobName = blobName
        };
    }

    private async Task<AddImageAssetCommandResult> ProcessBinaryData(AddImageAssetCommand request, CancellationToken cancellationToken)
    {
        var fileName = request.BinaryData!.FileName;
        var contentType = request.BinaryData!.ContentType;
        var binary = request.BinaryData.GetByteArray();

        var extension = Path.GetExtension(fileName);
        var mediaName = $"{Guid.NewGuid():N}.{extension}".ToLower();

        var blobName = $"images/{mediaName}";
        var destinationPath = $"content/assets/{blobName}";

        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        using var stream = new MemoryStream(binary);

        await azureBlob.UploadFile(stream, destinationPath, contentType, cancellationToken);
        LoggerService.LogInformation($"New image asset has been saved. Path: {destinationPath}.");

        return new AddImageAssetCommandResult
        {
            BlobName = blobName
        };
    }
}