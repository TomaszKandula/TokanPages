using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.User;
using TokanPages.Services.AzureStorageService.Abstractions;

namespace TokanPages.Backend.Application.Users.Queries;

public class GetUserImageQueryHandler : RequestHandler<GetUserImageQuery, FileContentResult>
{
    private readonly IUserRepository _userRepository;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public GetUserImageQueryHandler(ILoggerService loggerService, IAzureBlobStorageFactory azureBlobStorageFactory, 
        IUserRepository userRepository) : base(loggerService)
    {
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _userRepository = userRepository;
    }

    public override async Task<FileContentResult> Handle(GetUserImageQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserDetails(request.Id);
        if (user == null)
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        var sourcePath = $"content/users/{request.Id}/{request.BlobName}";

        var streamContent = await azureBlob.OpenRead(sourcePath, cancellationToken);
        if (streamContent is null)
            throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

        if (streamContent.Content is null)
            throw new BusinessException(nameof(ErrorCodes.ASSET_NOT_FOUND), ErrorCodes.ASSET_NOT_FOUND);

        if (streamContent.ContentType is null)
            throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

        var memoryStream = new MemoryStream();
        await streamContent.Content.CopyToAsync(memoryStream, cancellationToken);
        return new FileContentResult(memoryStream.ToArray(), streamContent.ContentType);
    }
}