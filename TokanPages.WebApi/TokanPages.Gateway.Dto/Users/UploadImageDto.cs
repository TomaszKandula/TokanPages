using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TokanPages.Gateway.Dto.Users;

/// <summary>
/// Use it when you want to upload user image.
/// </summary>
public class UploadImageDto
{
    /// <summary>
    /// File encoded in Base64.
    /// </summary>
    public string? Base64Data { get; set; }

    /// <summary>
    /// Binary data.
    /// </summary>
    [DataType(DataType.Upload)]
    public IFormFile? BinaryData { get; set; }
}