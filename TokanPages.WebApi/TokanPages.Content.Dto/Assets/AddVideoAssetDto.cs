using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Domain.Enums;

namespace TokanPages.Content.Dto.Assets;

/// <summary>
/// Use it when you want to add video asset.
/// </summary>
[ExcludeFromCodeCoverage]
public class AddVideoAssetDto
{
    /// <summary>
    /// Mandatory.
    /// </summary>
    public ProcessingTarget Target { get; set; }

    /// <summary>
    /// Mandatory.
    /// </summary>
    [DataType(DataType.Upload)]
    public IFormFile? BinaryData { get; set; }
}