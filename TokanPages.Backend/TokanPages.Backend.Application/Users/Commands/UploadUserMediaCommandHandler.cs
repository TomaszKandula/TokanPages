using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class UploadUserMediaCommandHandler : RequestHandler<UploadUserMediaCommand, UploadUserMediaCommandResult>
{
    private readonly IUserService _userService;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public UploadUserMediaCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureBlobStorageFactory azureBlobStorageFactory, IUserService userService) : base(databaseContext, loggerService)
    {
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _userService = userService;
    }

    public override async Task<UploadUserMediaCommandResult> Handle(UploadUserMediaCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(request.UserId, false, cancellationToken);
        var azureBlob = _azureBlobStorageFactory.Create();
        var stream = new MemoryStream(request.Data);

        var mediaName = $"{Guid.NewGuid():N}_{request.MediaName}".ToUpper();
        var blobName = $"{request.MediaType}/{mediaName}";
        var destinationPath = $"content/users/{user.Id}/{blobName}";

        await azureBlob.UploadFile(stream, destinationPath, cancellationToken: cancellationToken);
        LoggerService.LogInformation($"New user media file has been saved in storage. Path: {destinationPath}.");

        await DatabaseUpdate(blobName, request.MediaTarget, user.Id, cancellationToken);
        LoggerService.LogInformation($"User media name has been saved in database. Name: {blobName}.");

        return new UploadUserMediaCommandResult { BlobName = blobName } ;
    }

    private async Task DatabaseUpdate(string blobName, UserMedia mediaTarget, Guid userId, CancellationToken cancellationToken)
    {
        var userInfo = await DatabaseContext.UserInfo
            .SingleOrDefaultAsync(info => info.UserId == userId, cancellationToken);

        if (userInfo is null)
            throw new GeneralException(nameof(ErrorCodes.ERROR_UNEXPECTED));

        switch (mediaTarget)
        {
            case UserMedia.UserImage:
                userInfo.UserImageName = blobName;
                await DatabaseContext.SaveChangesAsync(cancellationToken);
                break;
            case UserMedia.UserVideo:
                userInfo.UserVideoName = blobName;
                await DatabaseContext.SaveChangesAsync(cancellationToken);
                break;
            case UserMedia.NotSpecified:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(mediaTarget), mediaTarget, null);
        }
    }
}