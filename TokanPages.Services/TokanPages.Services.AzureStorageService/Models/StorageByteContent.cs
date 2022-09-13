using System;
using System.Diagnostics.CodeAnalysis;

namespace TokanPages.Services.AzureStorageService.Models;

[ExcludeFromCodeCoverage]
public class StorageByteContent
{
    public BinaryData? Content { get; set; }

    public string? ContentType { get; set; }
}