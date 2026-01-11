using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.Articles;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class AddArticleCommandHandler : RequestHandler<AddArticleCommand, Guid>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public AddArticleCommandHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService, IUserService userService, 
        IDateTimeService dateTimeService, IAzureBlobStorageFactory azureBlobStorageFactory) : base(operationsDbContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
        _azureBlobStorageFactory = azureBlobStorageFactory;
    }

    public override async Task<Guid> Handle(AddArticleCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var newArticle = new Article
        {
            Title = request.Title,
            Description = request.Description,
            IsPublished = false,
            ReadCount = 0,
            CreatedBy = userId,
            CreatedAt = _dateTimeService.Now,
            UserId = userId,
            LanguageIso = "ENG"
        };

        await OperationsDbContext.Articles.AddAsync(newArticle, cancellationToken);
        await OperationsDbContext.SaveChangesAsync(cancellationToken);

        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        var textDestinationPath = $"content\\articles\\{newArticle.Id}\\text.json";
        var imageDestinationPath = $"content\\articles\\{newArticle.Id}\\image.jpg";

        await azureBlob.UploadContent(request.TextToUpload!, textDestinationPath, cancellationToken);
        await azureBlob.UploadContent(request.ImageToUpload!, imageDestinationPath, cancellationToken);

        return newArticle.Id;
    }
}