using TokanPages.Backend.Utility.Abstractions;

namespace TokanPages.Services.AzureStorageService.Abstractions;

public interface IAzureBlobStorageFactory
{
    IAzureBlobStorage Create(ILoggerService loggerService);

    IAzureBlobStorage Create(string containerName, ILoggerService loggerService);
}