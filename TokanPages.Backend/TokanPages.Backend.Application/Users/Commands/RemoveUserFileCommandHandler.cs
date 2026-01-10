using MediatR;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class RemoveUserFileCommandHandler : RequestHandler<RemoveUserFileCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public RemoveUserFileCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService, IAzureBlobStorageFactory azureBlobStorageFactory) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _azureBlobStorageFactory = azureBlobStorageFactory;
    }

    public override async Task<Unit> Handle(RemoveUserFileCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        var folder = request.Type switch
        {
            UserFile.Image => "images",
            UserFile.Audio => "audio",
            UserFile.Video => "videos",
            UserFile.Document => "documents",
            UserFile.Application => "applications",
            _ => throw new ArgumentOutOfRangeException()
        };

        var destinationPath = $"content/users/{userId}/files/{folder}/{request.UniqueBlobName}";
        LoggerService.LogInformation($"Removing user file, path: {destinationPath}");
        await azureBlob.DeleteFile(destinationPath, cancellationToken);

        return Unit.Value;
    }
}