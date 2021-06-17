using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Storage.Settings
{
    [ExcludeFromCodeCoverage]
    public class AzureStorageSettings
    {
        public string BaseUrl { get; set; }

        public string ContainerName { get; set; }
        
        public string ConnectionString { get; set; }
    }
}
