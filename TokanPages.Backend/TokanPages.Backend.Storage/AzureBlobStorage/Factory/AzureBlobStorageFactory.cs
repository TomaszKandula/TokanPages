namespace TokanPages.Backend.Storage.AzureBlobStorage.Factory
{
    public class AzureBlobStorageFactory : AzureBlobStorageObject, IAzureBlobStorageFactory
    {
        private readonly string FConnectionString;

        private readonly string FContainerName;

        public AzureBlobStorageFactory(string AConnectionString, string AContainerName = null)
        {
            FConnectionString = AConnectionString;
            FContainerName = AContainerName;
        }

        public AzureBlobStorageFactory() { }

        public override IAzureBlobStorage Create() 
            => new AzureBlobStorage(FConnectionString, FContainerName);

        public override IAzureBlobStorage Create(string AContainerName) 
            => new AzureBlobStorage(FConnectionString, AContainerName);
    }
}