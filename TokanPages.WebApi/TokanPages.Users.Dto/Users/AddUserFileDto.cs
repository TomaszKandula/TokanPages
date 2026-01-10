using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Users.Dto.Users;

/// <summary>
/// Use it when you want to upload user file.
/// </summary>
[ExcludeFromCodeCoverage]
public class AddUserFileDto
{
    /// <summary>
    /// User file type (image, audio, video, document, application).
    /// </summary>
    public UserFile Type { get; set; }

    /// <summary>
    /// Binary data.
    /// </summary>
    [DataType(DataType.Upload)]
    public IFormFile? BinaryData { get; set; }
}