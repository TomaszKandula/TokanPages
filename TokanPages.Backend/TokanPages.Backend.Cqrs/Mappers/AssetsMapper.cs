namespace TokanPages.Backend.Cqrs.Mappers;

using System.Diagnostics.CodeAnalysis;
using Dto.Assets;
using Core.Extensions;
using Handlers.Commands.Assets;

[ExcludeFromCodeCoverage]
public static class AssetsMapper
{
    public static AddSingleAssetCommand MapToAddSingleAssetCommand(AddSingleAssetDto model) => new()
    {
        MediaName = model.Data!.FileName,
        MediaType = model.Data.ContentType.ToMediaType(),
        Data = model.Data.GetByteArray()
    };
}