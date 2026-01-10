using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.AzureStorageService.Models;

[ExcludeFromCodeCoverage]
public class StorageListingOptions
{
    public string Prefix { get; set; } = string.Empty;

    public string? ExcludeByPath { get; set; } 

    public string? IncludeByPath { get; set; }

    public int PageSize { get; set; } = 10;
}