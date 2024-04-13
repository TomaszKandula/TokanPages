using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Services.AzureStorageService.Abstractions;

namespace TokanPages.Services.AzureStorageService;

public class AzureBlobStorageFactory : IAzureBlobStorageFactory
{
    private readonly string _connectionString;

    private readonly string _containerName;

    public AzureBlobStorageFactory(string connectionString, string containerName = "")
    {
        _connectionString = connectionString;
        _containerName = containerName;
    }

    public IAzureBlobStorage Create(ILoggerService loggerService) 
        => new AzureBlobStorage(_connectionString, _containerName, loggerService);

    public IAzureBlobStorage Create(string containerName, ILoggerService loggerService) 
        => new AzureBlobStorage(_connectionString, containerName, loggerService);
}