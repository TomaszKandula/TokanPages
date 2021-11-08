namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Database;
    using Core.Exceptions;
    using Shared.Resources;
    using Services.UserServiceProvider;
    using Core.Utilities.LoggerService;
    using Core.Utilities.DateTimeService;
    using Storage.AzureBlobStorage.Factory;
    using MediatR;

    public class UpdateArticleContentCommandHandler : TemplateHandler<UpdateArticleContentCommand, Unit>
    {
        private readonly IUserServiceProvider _userServiceProvider;
        
        private readonly IDateTimeService _dateTimeService;
        
        private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;
        
        public UpdateArticleContentCommandHandler(DatabaseContext databaseContext, ILoggerService loggerService,
            IUserServiceProvider userServiceProvider, IDateTimeService dateTimeService, 
            IAzureBlobStorageFactory azureBlobStorageFactory) : base(databaseContext, loggerService)
        {
            _userServiceProvider = userServiceProvider;
            _dateTimeService = dateTimeService;
            _azureBlobStorageFactory = azureBlobStorageFactory;
        }

        public override async Task<Unit> Handle(UpdateArticleContentCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userServiceProvider.GetUserId();
            if (userId == null)
                throw new BusinessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

            var articles = await DatabaseContext.Articles
                .Where(articles => articles.Id == request.Id)
                .ToListAsync(cancellationToken);

            if (!articles.Any())
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

            var currentArticle = articles.First();

            currentArticle.Title = request.Title ?? currentArticle.Title;
            currentArticle.Description = request.Description ?? currentArticle.Description;
            currentArticle.UpdatedAt = request.Title != null && request.Description != null
                ? _dateTimeService.Now
                : currentArticle.UpdatedAt;

            await DatabaseContext.SaveChangesAsync(cancellationToken);
            return await Task.FromResult(Unit.Value);
        }
    }
}