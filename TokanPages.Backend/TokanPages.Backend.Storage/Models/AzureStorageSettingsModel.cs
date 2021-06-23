using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Backend.Storage.Models
{
    [ExcludeFromCodeCoverage]
    public class AzureStorageSettingsModel
    {
        public string BaseUrl { get; set; }

        public string ContainerName { get; set; }
        
        public string ConnectionString { get; set; }
    }
}
