using MediatR;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Abstractions;

namespace TokanPages.Backend.Application.Logger.Commands;

public class UploadLogFileCommandHandler : RequestHandler<UploadLogFileCommand, Unit>
{
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public UploadLogFileCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureBlobStorageFactory azureBlobStorageFactory) : base(databaseContext, loggerService) 
        => _azureBlobStorageFactory = azureBlobStorageFactory;

    public override async Task<Unit> Handle(UploadLogFileCommand request, CancellationToken cancellationToken)
    {
        var fileName = request.Data!.FileName;
        var contentType = request.Data!.ContentType;
        var binary = request.Data.GetByteArray();

        var destinationPath = $"logs/{request.CatalogName}/{fileName}";
        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        using var stream = new MemoryStream(binary);

        await azureBlob.UploadFile(stream, destinationPath, contentType, cancellationToken);
        LoggerService.LogInformation($"New log file has been saved. Path: {destinationPath}.");

        return new Unit();
    }
}