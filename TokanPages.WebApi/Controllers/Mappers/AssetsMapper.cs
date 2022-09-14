using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Assets.Commands;
using TokanPages.Backend.Core.Extensions;
using TokanPages.WebApi.Dto.Assets;

namespace TokanPages.WebApi.Controllers.Mappers;

/// <summary>
/// Assets mapper
/// </summary>
[ExcludeFromCodeCoverage]
public static class AssetsMapper
{
    /// <summary>
    /// Maps request DTO to given command
    /// </summary>
    /// <param name="model">AddSingleAssetDto</param>
    /// <returns>AddSingleAssetCommand</returns>
    public static AddSingleAssetCommand MapToAddSingleAssetCommand(AddSingleAssetDto model) => new()
    {
        MediaName = model.Data!.FileName,
        MediaType = model.Data.ContentType.ToMediaType(),
        Data = model.Data.GetByteArray()
    };
}