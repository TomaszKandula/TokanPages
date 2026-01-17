using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;

namespace TokanPages.Backend.Application.Content.Cached.Queries;

public class GetFileByNameQueryHandler : RequestHandler<GetFileByNameQuery, FileContentResult>
{
    public GetFileByNameQueryHandler(OperationDbContext operationDbContext, ILoggerService loggerService) 
        : base(operationDbContext, loggerService) { }

    public override async Task<FileContentResult> Handle(GetFileByNameQuery request, CancellationToken cancellationToken)
    {
        var pathToFolder = $"{AppDomain.CurrentDomain.BaseDirectory}cached";
        if (!Directory.Exists(pathToFolder))
            throw new GeneralException(nameof(ErrorCodes.MISSING_CACHE_FOLDER), ErrorCodes.MISSING_CACHE_FOLDER);

        var name = string.IsNullOrWhiteSpace(request.FileName) ? "index.html" : request.FileName;
        var fullFilePath = $"{pathToFolder}{Path.DirectorySeparatorChar}{name}";
        if (!File.Exists(fullFilePath))
            throw new GeneralException(nameof(ErrorCodes.MISSING_CACHE_FILE), ErrorCodes.MISSING_CACHE_FILE);

        var contentType = GetMimeType(name);
        var file = await File.ReadAllBytesAsync(fullFilePath, cancellationToken);
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