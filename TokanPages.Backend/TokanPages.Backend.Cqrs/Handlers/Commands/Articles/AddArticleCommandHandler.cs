namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

using System;
using System.Threading;
using System.Threading.Tasks;
using Database;
using Core.Exceptions;
using Shared.Resources;
using Services.UserService;
using Core.Utilities.LoggerService;
using Core.Utilities.DateTimeService;
using Services.AzureStorageService.AzureBlobStorage.Factory;

public class AddArticleCommandHandler : RequestHandler<AddArticleCommand, Guid>
{
    private readonly IUserService _userService;
        
    private readonly IDateTimeService _dateTimeService;
        
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;
        
    public AddArticleCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService, IUserService userService, 
        IDateTimeService dateTimeService, IAzureBlobStorageFactory azureBlobStorageFactory) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
        _azureBlobStorageFactory = azureBlobStorageFactory;
    }

    public override async Task<Guid> Handle(AddArticleCommand request, CancellationToken cancellationToken)
    {
        var userId = await _userService.GetUserId();
        if (userId == null)
            throw new AccessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

        var newArticle = new Domain.Entities.Articles
        {
            Title = request.Title,
            Description = request.Description,
            IsPublished = false,
            ReadCount = 0,
            CreatedAt = _dateTimeService.Now,
            UpdatedAt = null,
            UserId = (Guid) userId
        };

        await DatabaseContext.Articles.AddAsync(newArticle, cancellationToken);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        var azureBlob = _azureBlobStorageFactory.Create();
        var textDestinationPath = $"content\\articles\\{newArticle.Id}\\text.json";
        var imageDestinationPath = $"content\\articles\\{newArticle.Id}\\image.jpg";

        await azureBlob.UploadContent(request.TextToUpload, textDestinationPath, cancellationToken);
        await azureBlob.UploadContent(request.ImageToUpload, imageDestinationPath, cancellationToken);
            
        return await Task.FromResult(newArticle.Id);
    }
}