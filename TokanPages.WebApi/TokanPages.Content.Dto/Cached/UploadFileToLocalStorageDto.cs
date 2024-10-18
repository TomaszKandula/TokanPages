using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace TokanPages.Content.Dto.Cached;

/// <summary>
/// Use it when you want to add file to local cache directory.
/// </summary>
[ExcludeFromCodeCoverage]
public class UploadFileToLocalStorageDto
{
    /// <summary>
    /// Binary data.
    /// </summary>
    [DataType(DataType.Upload)]
    public IFormFile? BinaryData { get; set; }
}