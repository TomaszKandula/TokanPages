using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.WebApi.Dto.Users;

/// <summary>
/// Use it when you want to upload media file (image/video).
/// </summary>
[ExcludeFromCodeCoverage]
public class UploadUserMediaDto
{
    /// <summary>
    /// User ID.
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Target (image/video).
    /// </summary>
    public UserMedia MediaTarget { get; set; }

    /// <summary>
    /// File data.
    /// </summary>
    [DataType(DataType.Upload)]
    public IFormFile? Data { get; set; }
}