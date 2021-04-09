namespace TokanPages.Backend.Storage.AzureBlobStorage.Factory
{
    public class AzureBlobStorageFactory : IAzureBlobStorageFactory
    {
        private readonly string FConnectionString;
        private readonly string FContainerName;

        public AzureBlobStorageFactory(string AConnectionString, string AContainerName = null)
        {
            FConnectionString = AConnectionString;
            FContainerName = AContainerName;
        }
        
        public IAzureBlobStorage Create() 
            => new AzureBlobStorage(FConnectionString, FContainerName);

        public IAzureBlobStorage Create(string AContainerName) 
            => new AzureBlobStorage(FConnectionString, FContainerName);
    }
}
