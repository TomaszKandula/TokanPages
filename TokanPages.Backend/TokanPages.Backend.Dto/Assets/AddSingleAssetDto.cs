namespace TokanPages.Backend.Dto.Assets;

using System.Diagnostics.CodeAnalysis;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

/// <summary>
/// Use it when you want to add an asset (image/video) 
/// </summary>
[ExcludeFromCodeCoverage]
public class AddSingleAssetDto
{
    /// <summary>
    /// Mandatory
    /// </summary>
    [DataType(DataType.Upload)]
    public IFormFile? Data { get; set; }
}