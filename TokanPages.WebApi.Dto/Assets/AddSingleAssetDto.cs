using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace TokanPages.WebApi.Dto.Assets;

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