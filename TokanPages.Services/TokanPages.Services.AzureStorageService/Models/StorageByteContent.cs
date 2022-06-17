namespace TokanPages.Services.AzureStorageService.Models;

using System;
using System.Diagnostics.CodeAnalysis;

[ExcludeFromCodeCoverage]
public class StorageByteContent
{
    public BinaryData? Content { get; set; }

    public string? ContentType { get; set; }
}