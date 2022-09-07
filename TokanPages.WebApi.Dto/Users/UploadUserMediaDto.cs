using TokanPages.Backend.Domain.Enums;

namespace TokanPages.WebApi.Dto.Users;

using System;
using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

/// <summary>
/// Use it when you want to update existing user avatar
/// </summary>
[ExcludeFromCodeCoverage]
public class UploadUserMediaDto
{
    /// <summary>
    /// Optional
    /// </summary>
    public Guid? UserId { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    public UserMedia MediaTarget { get; set; }

    /// <summary>
    /// Mandatory
    /// </summary>
    [DataType(DataType.Upload)]
    public IFormFile? Data { get; set; }
}