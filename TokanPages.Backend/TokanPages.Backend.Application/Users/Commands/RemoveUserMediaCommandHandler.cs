using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class RemoveUserMediaCommandHandler : RequestHandler<RemoveUserMediaCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public RemoveUserMediaCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IUserService userService, IAzureBlobStorageFactory azureBlobStorageFactory) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _azureBlobStorageFactory = azureBlobStorageFactory;
    }

    public override async Task<Unit> Handle(RemoveUserMediaCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(cancellationToken: cancellationToken);
        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);

        var destinationPath = $"content/users/{user.Id}/{request.UniqueBlobName}";

        var userInfo = await DatabaseContext.UserInfo
            .Where(userInfo => userInfo.UserId == user.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (userInfo is null)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        userInfo.UserImageName = userInfo.UserImageName == request.UniqueBlobName ? null : userInfo.UserImageName;
        userInfo.UserVideoName = userInfo.UserVideoName == request.UniqueBlobName ? null : userInfo.UserVideoName;

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        await azureBlob.DeleteFile(destinationPath, cancellationToken);

        return Unit.Value;
    }
}