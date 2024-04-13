using TokanPages.Backend.Core.Utilities.LoggerService;

namespace TokanPages.Services.AzureStorageService.Abstractions;

public interface IAzureBlobStorageFactory
{
    IAzureBlobStorage Create(ILoggerService loggerService);

    IAzureBlobStorage Create(string containerName, ILoggerService loggerService);
}