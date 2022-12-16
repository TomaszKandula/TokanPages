namespace TokanPages.Services.AzureStorageService.Abstractions;

public interface IAzureBlobStorageFactory
{
    IAzureBlobStorage Create();

    IAzureBlobStorage Create(string containerName);
}