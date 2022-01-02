namespace TokanPages.Services.AzureStorageService.AzureBlobStorage.Factory;

public class AzureBlobStorageFactory : IAzureBlobStorageFactory
{
    private readonly string _connectionString;

    private readonly string _containerName;

    public AzureBlobStorageFactory(string connectionString, string containerName = null)
    {
        _connectionString = connectionString;
        _containerName = containerName;
    }

    public AzureBlobStorageFactory() { }

    public IAzureBlobStorage Create() 
        => new AzureBlobStorage(_connectionString, _containerName);

    public IAzureBlobStorage Create(string containerName) 
        => new AzureBlobStorage(_connectionString, containerName);
}