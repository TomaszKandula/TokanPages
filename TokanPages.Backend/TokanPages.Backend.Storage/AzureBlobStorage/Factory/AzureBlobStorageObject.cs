namespace TokanPages.Backend.Storage.AzureBlobStorage.Factory
{
    public abstract class AzureBlobStorageObject
    {
        public abstract IAzureBlobStorage Create();

        public abstract IAzureBlobStorage Create(string AContainerName);
    }
}