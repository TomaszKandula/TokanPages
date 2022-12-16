using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleContentCommandHandler : RequestHandler<UpdateArticleContentCommand, Unit>
{
    private readonly IUserService _userService;
        
    private readonly IDateTimeService _dateTimeService;
        
    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;
        
    public UpdateArticleContentCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService,
        IUserService userService, IDateTimeService dateTimeService, 
        IAzureBlobStorageFactory azureBlobStorageFactory) : base(databaseContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
        _azureBlobStorageFactory = azureBlobStorageFactory;
    }

    public override async Task<Unit> Handle(UpdateArticleContentCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetActiveUser(null, false, cancellationToken);
        var article = await DatabaseContext.Articles
            .Where(articles => articles.UserId == user.Id)
            .Where(articles => articles.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (article is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        var azureBlob = _azureBlobStorageFactory.Create();
        if (!string.IsNullOrEmpty(request.TextToUpload))
        {
            var textDestinationPath = $"content\\articles\\{request.Id}\\text.json";
            await azureBlob.UploadContent(request.TextToUpload, textDestinationPath, cancellationToken);
        }

        if (!string.IsNullOrEmpty(request.ImageToUpload))
        {
            var imageDestinationPath = $"content\\articles\\{request.Id}\\image.jpg";
            await azureBlob.UploadContent(request.ImageToUpload, imageDestinationPath, cancellationToken);
        }

        article.Title = request.Title ?? article.Title;
        article.Description = request.Description ?? article.Description;
        article.UpdatedAt = request.Title != null && request.Description != null
            ? _dateTimeService.Now
            : article.UpdatedAt;

        await DatabaseContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}