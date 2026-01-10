using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.AzureStorageService.Models;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserFileListQueryHandler : RequestHandler<GetUserFileListQuery, GetUserFileListQueryResult>
{
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IUserService _userService;

    public GetUserFileListQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureBlobStorageFactory azureBlobStorageFactory, IUserService userService) 
        : base(databaseContext, loggerService)
    {
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _userService = userService;
    }

    public override async Task<GetUserFileListQueryResult> Handle(GetUserFileListQuery request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId(); 
        var prefixPath = $"content/users/{userId}/files/";
        var result = await GetBlobListing(request.Type, prefixPath, cancellationToken);

        return new GetUserFileListQueryResult
        {
            FileBlobs = result
        };
    }

    private async Task<List<string>> GetBlobListing(UserFile type, string prefix, CancellationToken cancellationToken)
    {
        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        var options = new StorageListingOptions { Prefix = prefix };

        options.IncludeByPath = type switch
        {
            UserFile.Image => "images/",
            UserFile.Audio => "audio/",
            UserFile.Video => "videos/",
            UserFile.Document => "documents/",
            UserFile.Application => "applications/",
            _ => options.IncludeByPath
        };

        return await azureBlob.GetBlobListing(options, cancellationToken: cancellationToken);
    }
}