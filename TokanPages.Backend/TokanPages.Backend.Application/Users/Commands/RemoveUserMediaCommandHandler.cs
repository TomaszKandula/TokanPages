namespace TokanPages.Backend.Application.Users.Commands;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Services.UserService;
using Core.Utilities.LoggerService;
using Services.AzureStorageService.Factory;
using MediatR;

public class RemoveUserMediaCommandHandler : Application.RequestHandler<RemoveUserMediaCommand, Unit>
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
        var azureBlob = _azureBlobStorageFactory.Create();

        var destinationPath = $"content/users/{user.Id}/{request.UniqueBlobName}";

        var userInfo = await DatabaseContext.UserInfo
            .Where(userInfo => userInfo.UserId == user.Id)
            .SingleOrDefaultAsync(cancellationToken);

        userInfo.UserImageName = userInfo.UserImageName == request.UniqueBlobName ? null : userInfo.UserImageName;
        userInfo.UserVideoName = userInfo.UserVideoName == request.UniqueBlobName ? null : userInfo.UserVideoName;

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        await azureBlob.DeleteFile(destinationPath, cancellationToken);

        return Unit.Value;
    }
}