using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace TokanPages.Logger.Dto;

/// <summary>
/// Use it when you want to upload log file (Redis, etc.).
/// </summary>
[ExcludeFromCodeCoverage]
public class UploadLogFileDto
{
    /// <summary>
    /// Binary data. Existing file is always overwritten.
    /// </summary>
    [DataType(DataType.Upload)]
    public IFormFile? Data { get; set; }
}