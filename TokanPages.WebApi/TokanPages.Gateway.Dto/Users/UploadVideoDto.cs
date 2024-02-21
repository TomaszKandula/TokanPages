using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace TokanPages.Gateway.Dto.Users;

/// <summary>
/// Use it when you want to upload presentation video.
/// </summary>
public class UploadVideoDto
{
    /// <summary>
    /// Binary data.
    /// </summary>
    [DataType(DataType.Upload)]
    public IFormFile? BinaryData { get; set; }
}