using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Logger.Queries;

public class GetLogFileContentQueryHandler : RequestHandler<GetLogFileContentQuery, FileContentResult>
{
    public GetLogFileContentQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) 
        : base(databaseContext, loggerService) { }

    public override async Task<FileContentResult> Handle(GetLogFileContentQuery request, CancellationToken cancellationToken)
    {
        var pathToFolder = $"{AppDomain.CurrentDomain.BaseDirectory}logs";
        if (!Directory.Exists(pathToFolder))
            throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

        var fullFilePath = $"{pathToFolder}{Path.DirectorySeparatorChar}{request.LogFileName}";
        if (!File.Exists(fullFilePath))
            throw new BusinessException(nameof(ErrorCodes.FILE_NOT_FOUND), ErrorCodes.FILE_NOT_FOUND);

        var fileContent = await File.ReadAllBytesAsync(fullFilePath, cancellationToken);
        return new FileContentResult(fileContent, "text/plain");
    }
}