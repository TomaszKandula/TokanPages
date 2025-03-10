﻿using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Persistence.Database;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

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
        var user = await _userService.GetActiveUser(null, false, cancellationToken);
        var newArticle = new Domain.Entities.Article.Articles
        {
            Title = request.Title,
            Description = request.Description,
            IsPublished = false,
            ReadCount = 0,
            CreatedBy = user.Id,
            CreatedAt = _dateTimeService.Now,
            UserId = user.Id,
            LanguageIso = "ENG"
        };

        await DatabaseContext.Articles.AddAsync(newArticle, cancellationToken);
        await DatabaseContext.SaveChangesAsync(cancellationToken);

        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
        var textDestinationPath = $"content\\articles\\{newArticle.Id}\\text.json";
        var imageDestinationPath = $"content\\articles\\{newArticle.Id}\\image.jpg";

        await azureBlob.UploadContent(request.TextToUpload!, textDestinationPath, cancellationToken);
        await azureBlob.UploadContent(request.ImageToUpload!, imageDestinationPath, cancellationToken);

        return newArticle.Id;
    }
}