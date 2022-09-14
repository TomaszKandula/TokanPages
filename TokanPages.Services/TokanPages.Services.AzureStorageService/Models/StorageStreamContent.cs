using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.AzureStorageService.Models;

[ExcludeFromCodeCoverage]
public class StorageStreamContent
{
    public Stream? Content { get; set; }

    public string? ContentType { get; set; }
}