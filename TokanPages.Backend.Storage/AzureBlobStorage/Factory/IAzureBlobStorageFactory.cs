namespace TokanPages.Backend.Storage.AzureBlobStorage.Factory
{
    public interface IAzureBlobStorageFactory
    {
        IAzureBlobStorage Create();

        IAzureBlobStorage Create(string AContainerName);
    }
}
