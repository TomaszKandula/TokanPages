using System.Diagnostics.CodeAnalysis;
using TokanPages.Backend.Application.Content.Cached.Commands;
using TokanPages.Content.Dto.Cached;

namespace TokanPages.Content.Controllers.Mappers;

/// <summary>
/// Cached mapper.
/// </summary>
[ExcludeFromCodeCoverage]
public static class CachedMapper
{
    /// <summary>
    /// Map request DTO to a given command.
    /// </summary>
    /// <param name="model">Assets object.</param>
    /// <returns>Command object.</returns>
    public static UploadFileToLocalStorageCommand MapToUploadFileToLocalStorageCommand(UploadFileToLocalStorageDto model) => new()
    {
        BinaryData = model.BinaryData
    };
}