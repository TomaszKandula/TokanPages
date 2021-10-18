namespace TokanPages.Backend.Storage.AzureBlobStorage.Factory
{
    public class AzureBlobStorageFactory : AzureBlobStorageObject, IAzureBlobStorageFactory
    {
        private readonly string _connectionString;

        private readonly string _containerName;

        public AzureBlobStorageFactory(string connectionString, string containerName = null)
        {
            _connectionString = connectionString;
            _containerName = containerName;
        }

        public AzureBlobStorageFactory() { }

        public override IAzureBlobStorage Create() 
            => new AzureBlobStorage(_connectionString, _containerName);

        public override IAzureBlobStorage Create(string containerName) 
            => new AzureBlobStorage(_connectionString, containerName);
    }
}