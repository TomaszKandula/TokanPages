namespace TokanPages.Backend.Storage.AzureBobStorage.Factory
{
    public interface IAzureBlobStorageFactory
    {
        IAzureBlobStorage Create();
        IAzureBlobStorage Create(string AContainerName);
    }
}
