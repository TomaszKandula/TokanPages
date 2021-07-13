namespace TokanPages.Backend.Storage.Models
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public class AzureStorageSettingsModel
    {
        public string BaseUrl { get; set; }

        public string ContainerName { get; set; }
        
        public string ConnectionString { get; set; }
    }
}