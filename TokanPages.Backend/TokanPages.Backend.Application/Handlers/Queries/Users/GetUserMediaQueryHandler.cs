namespace TokanPages.Backend.Application.Handlers.Queries.Users;

using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Core.Exceptions;
using Shared.Resources;
using Core.Utilities.LoggerService;
using Services.AzureStorageService.Factory;

public class GetUserMediaQueryHandler : RequestHandler<GetUserMediaQuery, FileContentResult>
{
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public GetUserMediaQueryHandler(DatabaseContext databaseContext, ILoggerService loggerService, 
        IAzureBlobStorageFactory azureBlobStorageFactory) : base(databaseContext, loggerService)
        => _azureBlobStorageFactory = azureBlobStorageFactory;

    public override async Task<FileContentResult> Handle(GetUserMediaQuery request, CancellationToken cancellationToken)
    {
        var user = await DatabaseContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(users => users.Id == request.Id, cancellationToken);

        if (user == null)
            throw new AuthorizationException(nameof(ErrorCodes.USER_DOES_NOT_EXISTS), ErrorCodes.USER_DOES_NOT_EXISTS);

        var azureBlob = _azureBlobStorageFactory.Create();
        var sourcePath = $"content/users/{request.Id}/{request.BlobName}";

        var streamContent = await azureBlob.OpenRead(sourcePath, cancellationToken);
        if (streamContent is null)
            throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

        if (streamContent.Content is null)
            throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

        if (streamContent.ContentType is null)
            throw new BusinessException(nameof(ErrorCodes.ERROR_UNEXPECTED), ErrorCodes.ERROR_UNEXPECTED);

        var memoryStream = new MemoryStream();
        await streamContent.Content.CopyToAsync(memoryStream, cancellationToken);
        return new FileContentResult(memoryStream.ToArray(), streamContent.ContentType);
    }
}