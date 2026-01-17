using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;

namespace TokanPages.Backend.Application.Content.Cached.Commands;

public class UploadFileToLocalStorageCommandHandler : RequestHandler<UploadFileToLocalStorageCommand, UploadFileToLocalStorageCommandResult>
{
    private const decimal MaxDirectorySizeKb = 102400;

    public UploadFileToLocalStorageCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService) 
        : base(operationDbContext, loggerService) { }

    public override async Task<UploadFileToLocalStorageCommandResult> Handle(UploadFileToLocalStorageCommand request, CancellationToken cancellationToken)
    {
        var pathToFolder = $"{AppDomain.CurrentDomain.BaseDirectory}cached";
        if (!Directory.Exists(pathToFolder))
            Directory.CreateDirectory(pathToFolder);

        var fileName = request.BinaryData!.FileName;
        var contentType = request.BinaryData!.ContentType;
        var binary = request.BinaryData.GetByteArray();

        var fileSizeKb = binary.Length / 1024M;
        var currentDirSizeKb = await GetDirSizeInBytes(pathToFolder) / 1024M;
        var calculatedSizeKb = currentDirSizeKb + fileSizeKb;
        if (calculatedSizeKb > MaxDirectorySizeKb)
            throw new GeneralException(nameof(ErrorCodes.NO_FREE_SPACE), ErrorCodes.NO_FREE_SPACE);

        var fullFilePath = $"{pathToFolder}{Path.DirectorySeparatorChar}{fileName}";
        await File.WriteAllBytesAsync(fullFilePath, binary, cancellationToken);

        LoggerService.LogInformation($"File '{fileName}' has been saved to local directory. Content type: '{contentType}'.");

        var directorySize = await GetDirSizeInBytes(pathToFolder) / 1024M;
        var freeSpaceKb = MaxDirectorySizeKb - directorySize;
        var freeSpaceMb = freeSpaceKb / 1024M;
        const decimal maxDirectorySizeMb = MaxDirectorySizeKb / 1024M;

        return new UploadFileToLocalStorageCommandResult
        {
            UploadedFileSize = $"{fileSizeKb:###.##} kB",
            CurrentDirectorySize = $"{directorySize:###.##} kB",
            FreeSpace = $"{freeSpaceKb:###.##} kB ({freeSpaceMb:###.##} MB)",
            InternalDirectorySizeLimit = $"{MaxDirectorySizeKb:###.##} kB ({maxDirectorySizeMb} MB)"
        };
    }

    private static async Task<long> GetDirSizeInBytes(string path)
    {
        var dirInfo = new DirectoryInfo(path);
        return await Task.Run(() => dirInfo
            .EnumerateFiles( "*", SearchOption.AllDirectories)
            .Sum(file => file.Length));
    }
}