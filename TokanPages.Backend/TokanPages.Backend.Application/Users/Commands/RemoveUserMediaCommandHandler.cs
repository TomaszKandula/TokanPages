using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class RemoveUserMediaCommandHandler : RequestHandler<RemoveUserMediaCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public RemoveUserMediaCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IAzureBlobStorageFactory azureBlobStorageFactory) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _azureBlobStorageFactory = azureBlobStorageFactory;
    }

    public override async Task<Unit> Handle(RemoveUserMediaCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);

        var destinationPath = $"content/users/{userId}/{request.UniqueBlobName}";

        var userInfo = await OperationDbContext.UserInformation
            .Where(userInfo => userInfo.UserId == userId)
            .SingleOrDefaultAsync(cancellationToken);

        if (userInfo is null)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        userInfo.UserImageName = userInfo.UserImageName == request.UniqueBlobName ? null : userInfo.UserImageName;
        userInfo.UserVideoName = userInfo.UserVideoName == request.UniqueBlobName ? null : userInfo.UserVideoName;

        await OperationDbContext.SaveChangesAsync(cancellationToken);
        await azureBlob.DeleteFile(destinationPath, cancellationToken);

        return Unit.Value;
    }
}