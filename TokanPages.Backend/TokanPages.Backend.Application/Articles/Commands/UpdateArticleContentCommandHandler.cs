using MediatR;
using Microsoft.EntityFrameworkCore;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Persistence.Database;
using TokanPages.Persistence.Database.Contexts;
using TokanPages.Services.AzureStorageService.Abstractions;
using TokanPages.Services.UserService.Abstractions;

namespace TokanPages.Backend.Application.Articles.Commands;

public class UpdateArticleContentCommandHandler : RequestHandler<UpdateArticleContentCommand, Unit>
{
    private readonly IUserService _userService;

    private readonly IDateTimeService _dateTimeService;

    private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;

    public UpdateArticleContentCommandHandler(OperationsDbContext operationsDbContext, ILoggerService loggerService,
        IUserService userService, IDateTimeService dateTimeService, 
        IAzureBlobStorageFactory azureBlobStorageFactory) : base(operationsDbContext, loggerService)
    {
        _userService = userService;
        _dateTimeService = dateTimeService;
        _azureBlobStorageFactory = azureBlobStorageFactory;
    }

    public override async Task<Unit> Handle(UpdateArticleContentCommand request, CancellationToken cancellationToken)
    {
        var userId = _userService.GetLoggedUserId();
        var articleData = await OperationsDbContext.Articles
            .Where(article => article.UserId == userId)
            .Where(article => article.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        if (articleData is null)
            throw new BusinessException(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS), ErrorCodes.ARTICLE_DOES_NOT_EXISTS);

        var azureBlob = _azureBlobStorageFactory.Create(LoggerService);
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

        articleData.Title = request.Title ?? articleData.Title;
        articleData.Description = request.Description ?? articleData.Description;
        articleData.LanguageIso = request.LanguageIso ?? articleData.LanguageIso;
        articleData.UpdatedAt = request is { Title: not null, Description: not null }
            ? _dateTimeService.Now
            : articleData.UpdatedAt;

        articleData.ModifiedAt = _dateTimeService.Now;
        articleData.ModifiedBy = userId;

        await OperationsDbContext.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}