using System.Text.RegularExpressions;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.UserService.Abstractions;
using Microsoft.EntityFrameworkCore;
using TokanPages.Services.AzureStorageService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class UploadImageCommandHandler : RequestHandler<UploadImageCommand, UploadImageCommandResult>
{
    private readonly IUserService _userService;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public UploadImageCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureBlobStorageFactory azureBlobStorageFactory, IUserService userService) : base(databaseContext, loggerService)
    {
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _userService = userService;
    }

    public override async Task<UploadImageCommandResult> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        if (request.Base64Data is not null && request.Base64Data.Length > 0)
            return await ProcessBase64(request, cancellationToken);

        if (request.BinaryData is not null && request.BinaryData.Length > 0)
            return await ProcessBinaryData(request, cancellationToken);

        throw new GeneralException(nameof(ErrorCodes.ASSET_NOT_FOUND), ErrorCodes.ASSET_NOT_FOUND);
    }

    private async Task<UploadImageCommandResult> ProcessBase64(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(isTracking: false, cancellationToken: cancellationToken);

        var base64Data = request.Base64Data;
        var base64Raw = base64Data!.Split(",")[1];

        var regex = new Regex(@"data:(.*);base64");
        var match = regex.Match(base64Data);

        var contentType = match.Groups[1].Value;
        var extension = contentType.Split("/")[1];
        var binary = Convert.FromBase64String(base64Raw);

        var fileName = $"{Guid.NewGuid():N}.{extension}".ToLower();
        var blobName = $"images/{fileName}";
        var destinationPath = $"content/users/{user.Id}/{blobName}";

        var azureBlob = _azureBlobStorageFactory.Create();
        using var stream = new MemoryStream(binary);

        await azureBlob.UploadFile(stream, destinationPath, contentType, cancellationToken);
        LoggerService.LogInformation($"New image asset has been saved. Path: {destinationPath}.");

        if (request.SkipDb) 
            return new UploadImageCommandResult { BlobName = blobName };

        await DatabaseUpdate(blobName, user.Id, cancellationToken);
        LoggerService.LogInformation($"Image blob name has been saved in database. Name: {blobName}.");
        
        return new UploadImageCommandResult
        {
            BlobName = blobName
        };
    }

    private async Task<UploadImageCommandResult> ProcessBinaryData(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(isTracking: false, cancellationToken: cancellationToken);

        var fileName = request.BinaryData!.FileName;
        var contentType = request.BinaryData!.ContentType;
        var binary = request.BinaryData.GetByteArray();

        var extension = Path.GetExtension(fileName);
        var imageName = $"{Guid.NewGuid():N}.{extension}".ToLower();

        var blobName = $"images/{imageName}";
        var destinationPath = $"content/users/{user.Id}/{blobName}";

        var azureBlob = _azureBlobStorageFactory.Create();
        using var stream = new MemoryStream(binary);

        await azureBlob.UploadFile(stream, destinationPath, contentType, cancellationToken);
        LoggerService.LogInformation($"New image asset has been saved. Path: {destinationPath}.");

        if (request.SkipDb) 
            return new UploadImageCommandResult { BlobName = blobName };

        await DatabaseUpdate(blobName, user.Id, cancellationToken);
        LoggerService.LogInformation($"Image blob name has been saved in database. Name: {blobName}.");

        return new UploadImageCommandResult
        {
            BlobName = blobName
        };
    }

    private async Task DatabaseUpdate(string blobName, Guid userId, CancellationToken cancellationToken)
    {
        var userInfo = await DatabaseContext.UserInfo
            .SingleOrDefaultAsync(info => info.UserId == userId, cancellationToken);

        if (userInfo is not null)
        {
            userInfo.UserImageName = blobName;
            await DatabaseContext.SaveChangesAsync(cancellationToken);
        }
    }
}