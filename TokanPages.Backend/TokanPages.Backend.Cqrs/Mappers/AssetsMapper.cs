namespace TokanPages.Backend.Cqrs.Mappers;

using System.Diagnostics.CodeAnalysis;
using Core.Extensions;
using Dto.Assets;
using Handlers.Commands.Assets;

[ExcludeFromCodeCoverage]
public static class AssetsMapper
{
    public static AddSingleAssetCommand MapToAddSingleAssetCommand(AddSingleAssetDto model) => new()
    {
        MediaName = model.Data.FileName,
        MediaType = model.Data.ContentType,
        Data = model.Data.GetByteArray()
    };
}