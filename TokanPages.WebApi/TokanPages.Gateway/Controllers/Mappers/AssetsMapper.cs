using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Assets.Commands;
using TokanPages.Gateway.Dto.Assets;

namespace TokanPages.Gateway.Controllers.Mappers;

/// <summary>
/// Assets mapper.
/// </summary>
[ExcludeFromCodeCoverage]
public static class AssetsMapper
{
    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Assets object.</param>
    /// <returns>Command object.</returns>
    public static AddImageAssetCommand MapToAddImageAssetCommand(AddImageAssetDto model) => new()
    {
        Base64Data = model.Base64Data,
        BinaryData = model.BinaryData
    };

    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Assets object.</param>
    /// <returns>Command object.</returns>
    public static AddVideoAssetCommand MapToAddVideoAssetCommand(AddVideoAssetDto model) => new()
    {
        Target = model.Target,
        BinaryData = model.BinaryData
    };
}