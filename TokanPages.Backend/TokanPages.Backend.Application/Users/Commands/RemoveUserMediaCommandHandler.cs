using MediatR;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Commands;

public class RemoveUserMediaCommandHandler : RequestHandler<RemoveUserMediaCommand, Unit>
{
    private readonly IUserRepository _userRepository;

    private readonly IUserService _userService;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public RemoveUserMediaCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, 
        IUserService userService, IAzureBlobStorageFactory azureBlobStorageFactory, IUserRepository userRepository) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _userRepository = userRepository;
    }

    public override async Task<Unit> Handle(RemoveUserMediaCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        var destinationPath = $"content/users/{userId}/{request.UniqueBlobName}";

        var userDetails = await _userRepository.GetUserDetails(userId);
        if (userDetails is null)
            throw new BusinessException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        await _userRepository.ClearUserMedia(userId);
        await azureBlob.DeleteFile(destinationPath, cancellationToken);

        return Unit.Value;
    }
}