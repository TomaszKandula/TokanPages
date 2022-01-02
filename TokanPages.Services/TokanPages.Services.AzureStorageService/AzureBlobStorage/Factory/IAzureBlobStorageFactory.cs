namespace TokanPages.Services.AzureStorageService.AzureBlobStorage.Factory;

public interface IAzureBlobStorageFactory
{
    IAzureBlobStorage Create();

    IAzureBlobStorage Create(string containerName);
}