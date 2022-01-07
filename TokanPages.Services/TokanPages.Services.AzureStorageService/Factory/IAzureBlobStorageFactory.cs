namespace TokanPages.Services.AzureStorageService.Factory;

public interface IAzureBlobStorageFactory
{
    IAzureBlobStorage Create();

    IAzureBlobStorage Create(string containerName);
}