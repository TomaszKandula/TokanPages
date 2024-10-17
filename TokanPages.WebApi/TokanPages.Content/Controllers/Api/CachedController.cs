using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Attributes;
using TokanPages.Backend.Shared.Resources;

namespace TokanPages.Content.Controllers.Api;

/// <summary>
/// API endpoints definitions for cached files by SpaCachingService.
/// </summary>
///<remarks>
/// It uses Microsoft 'ResponseCache' for caching content.
/// </remarks>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/content/[controller]")]
public class CachedController : ControllerBase
{
    /// <summary>
    /// Returns requested file.
    /// </summary>
    /// <param name="fileName">Requested file name. If empty, then it fallbacks to 'index.html'.</param>
    /// <returns>File.</returns>
    [HttpGet]
    [ETagFilter]
    [Route("{fileName?}")]
    [ResponseCache(Location = ResponseCacheLocation.Any, NoStore = false, Duration = 86400, VaryByQueryKeys = new [] { "fileName" })]
    public async Task<FileContentResult> Get([FromRoute] string? fileName = null)
    {
        var pathToFolder = $"{AppDomain.CurrentDomain.BaseDirectory}cached";
        if (!Directory.Exists(pathToFolder))
            throw new GeneralException(nameof(ErrorCodes.MISSING_CACHE_FOLDER), ErrorCodes.MISSING_CACHE_FOLDER);

        var name = string.IsNullOrWhiteSpace(fileName) ? "index.html" : fileName;
        var fullFilePath = $"{pathToFolder}{Path.DirectorySeparatorChar}{name}";
        if (!System.IO.File.Exists(fullFilePath))
            throw new GeneralException(nameof(ErrorCodes.MISSING_CACHE_FILE), ErrorCodes.MISSING_CACHE_FILE);

        var contentType = GetMimeType(name);
        var file = await System.IO.File.ReadAllBytesAsync(fullFilePath);
        return new FileContentResult(file, contentType);
    }

    private static string GetMimeType(string fileName)
    {
        var provider = new FileExtensionContentTypeProvider();
        if (!provider.TryGetContentType(fileName, out var contentType))
            contentType = "application/octet-stream";

        return contentType;            
    }
}