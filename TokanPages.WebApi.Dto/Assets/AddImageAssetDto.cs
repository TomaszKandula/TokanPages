using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace TokanPages.WebApi.Dto.Assets;

/// <summary>
/// Use it when you want to add an image asset.
/// </summary>
[ExcludeFromCodeCoverage]
public class AddImageAssetDto
{
    /// <summary>
    /// Optional.
    /// </summary>
    public string? Base64Data { get; set; }

    /// <summary>
    /// Optional.
    /// </summary>
    [DataType(DataType.Upload)]
    public IFormFile? BinaryData { get; set; }
}