using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Contexts;
using TokanPages.Persistence.DataAccess.Repositories.Articles;
using TokanPages.Persistence.DataAccess.Repositories.Articles.Models;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class AddArticleCommandHandler : RequestHandler<AddArticleCommand, Guid>
{
    private readonly IUserService _userService;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    private readonly IArticlesRepository _articlesRepository;

    public AddArticleCommandHandler(OperationDbContext operationDbContext, ILoggerService loggerService, IUserService userService, 
        IAzureBlobStorageFactory azureBlobStorageFactory, IArticlesRepository articlesRepository) : base(operationDbContext, loggerService)
    {
        _userService = userService;
        _azureBlobStorageFactory = azureBlobStorageFactory;
        _articlesRepository = articlesRepository;
    }

    public override async Task<Guid> Handle(AddArticleCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var articleId = Guid.NewGuid();

        var input = new ArticleDataInputDto
        {
            ArticleId = articleId,
            Title = request.Title,
            Description = request.Description,
            LanguageIso = request.LanguageIso,
        };

        await _articlesRepository.CreateArticle(userId, input);

        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        var textDestinationPath = $"content\\articles\\{articleId}\\text.json";
        var imageDestinationPath = $"content\\articles\\{articleId}\\image.jpg";

        await azureBlob.UploadContent(request.TextToUpload, textDestinationPath, cancellationToken);
        await azureBlob.UploadContent(request.ImageToUpload, imageDestinationPath, cancellationToken);

        return articleId;
    }
}