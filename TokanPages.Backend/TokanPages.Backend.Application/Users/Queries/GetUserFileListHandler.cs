using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserFileListHandler : RequestHandler<GetUserFileListQuery, GetUserFileListResult>
{
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IUserService _userService;

    public GetUserFileListHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureBlobStorageFactory azureBlobStorageFactory, IUserService userService) 
        : base(databaseContext, loggerService)
    {
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _userService = userService;
    }

    public override async Task<GetUserFileListResult> Handle(GetUserFileListQuery request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();

        var prefixPath = $"content/users/{userId}/files/";
        var includeByPath = request.IsVideoFile ? "videos/" : string.Empty;
        var excludeByPath = request.IsVideoFile ? string.Empty : "videos/";

        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        var result = await azureBlob.GetBlobListing(
            prefix: prefixPath, 
            includeByPath: includeByPath, 
            excludeByPath: excludeByPath, 
            cancellationToken: cancellationToken);

        return new GetUserFileListResult
        {
            FileBlobs = result
        };
    }
}