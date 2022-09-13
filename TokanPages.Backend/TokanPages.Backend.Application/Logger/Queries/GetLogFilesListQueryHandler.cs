using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;

namespace TokanPages.Backend.Application.Logger.Queries;

public class GetLogFilesListQueryHandler : RequestHandler<GetLogFilesListQuery, GetLogFilesListQueryResult>
{
    public GetLogFilesListQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService) 
        : base(databaseContext, loggerService) { }

    public override async Task<GetLogFilesListQueryResult> Handle(GetLogFilesListQuery request, CancellationToken cancellationToken)
    {
        var pathToFolder = $"{AppDomain.CurrentDomain.BaseDirectory}logs";
        if (!Directory.Exists(pathToFolder))
            return new GetLogFilesListQueryResult();

        var fullPathFileList = Directory.EnumerateFiles(pathToFolder, "*.txt", SearchOption.TopDirectoryOnly);
        var logFiles = new List<string>();

        foreach (var item in fullPathFileList)
        {
            logFiles.Add(Path.GetFileName(item));
        }

        return await Task.FromResult(new GetLogFilesListQueryResult
        {
            LogFiles = logFiles.ToList()
        });
    }
}