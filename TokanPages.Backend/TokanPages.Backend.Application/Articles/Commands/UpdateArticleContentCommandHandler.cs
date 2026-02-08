using MediatR;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleContentCommandHandler : RequestHandler<UpdateArticleContentCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IArticlesRepository _articlesRepository;

    public UpdateArticleContentCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IUserService userService,
        IAzureBlobStorageFactory azureBlobStorageFactory, IArticlesRepository articlesRepository) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _articlesRepository = articlesRepository;
    }

    public override async Task<Unit> Handle(UpdateArticleContentCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        await _articlesRepository.UpdateArticleContent(userId, request.Id, request.Title, request.Description, request.LanguageIso);

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