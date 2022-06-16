namespace TokanPages.Backend.Shared.Models;

using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class AzureStorage
{
    public string BaseUrl { get; set; } = "";

    public string ContainerName { get; set; } = "";

    public string ConnectionString { get; set; } = "";

    public int MaxFileSizeUserMedia { get; set; }

    public int MaxFileSizeSingleAsset { get; set; }
}