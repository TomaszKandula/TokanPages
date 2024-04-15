using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Content.Assets.Commands;
using TokanPages.Content.Dto.Assets;

namespace TokanPages.Content.Controllers.Mappers;

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