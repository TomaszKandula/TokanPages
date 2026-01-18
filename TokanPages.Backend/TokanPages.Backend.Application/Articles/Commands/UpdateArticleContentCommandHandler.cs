using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleContentCommandHandler : RequestHandler<UpdateArticleContentCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IArticlesRepository _articlesRepository;

    public UpdateArticleContentCommandHandler(OperationDbContext operationDbContext, 
        ILoggerService loggerService, IUserService userService, IDateTimeService dateTimeService, 
        IAzureBlobStorageFactory azureBlobStorageFactory, IArticlesRepository articlesRepository) 
        : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _articlesRepository = articlesRepository;
    }

    public override async Task<Unit> Handle(UpdateArticleContentCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var dateTimeStamp = _dateTimeService.Now;
        var isSuccess = await _articlesRepository.UpdateArticleContent(userId, request.Id, dateTimeStamp, request.Title, request.Description, request.LanguageIso, cancellationToken);
        if (!isSuccess)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        var hasTextToUpload = !string.IsNullOrWhiteSpace(request.TextToUpload);
        var hasImageToUpload = !string.IsNullOrWhiteSpace(request.ImageToUpload);

        if (hasTextToUpload)
        {
            var textDestinationPath = $"content\\articles\\{request.Id}\\text.json";
            await azureBlob.UploadContent(request.TextToUpload!, textDestinationPath, cancellationToken);
        }

        if (hasImageToUpload)
        {
            var imageDestinationPath = $"content\\articles\\{request.Id}\\image.jpg";
            await azureBlob.UploadContent(request.ImageToUpload!, imageDestinationPath, cancellationToken);
        }

        return Unit.Value;
    }
}