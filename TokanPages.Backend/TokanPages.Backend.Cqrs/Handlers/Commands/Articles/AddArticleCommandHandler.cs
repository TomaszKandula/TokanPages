﻿namespace TokanPages.Backend.Cqrs.Handlers.Commands.Articles
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Database;
    using Core.Exceptions;
    using Shared.Resources;
    using Services.UserServiceProvider;
    using Core.Utilities.DateTimeService;
    using Storage.AzureBlobStorage.Factory;

    public class AddArticleCommandHandler : TemplateHandler<AddArticleCommand, Guid>
    {
        private readonly DatabaseContext _databaseContext;
        
        private readonly IUserServiceProvider _userServiceProvider;
        
        private readonly IDateTimeService _dateTimeService;
        
        private readonly IAzureBlobStorageFactory _azureBlobStorageFactory;
        
        public AddArticleCommandHandler(DatabaseContext databaseContext, IUserServiceProvider userServiceProvider, 
            IDateTimeService dateTimeService, IAzureBlobStorageFactory azureBlobStorageFactory) 
        {
            _databaseContext = databaseContext;
            _userServiceProvider = userServiceProvider;
            _dateTimeService = dateTimeService;
            _azureBlobStorageFactory = azureBlobStorageFactory;
        }

        public override async Task<Guid> Handle(AddArticleCommand request, CancellationToken cancellationToken)
        {
            var userId = await _userServiceProvider.GetUserId();
            if (userId == null)
                throw new BusinessException(nameof(ErrorCodes.ACCESS_DENIED), ErrorCodes.ACCESS_DENIED);

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

            await _databaseContext.Articles.AddAsync(newArticle, cancellationToken);
            await _databaseContext.SaveChangesAsync(cancellationToken);

            var azureBlob = _azureBlobStorageFactory.Create();
            var textDestinationPath = $"content\\articles\\{newArticle.Id}\\text.json";
            var imageDestinationPath = $"content\\articles\\{newArticle.Id}\\image.jpg";

            await azureBlob.UploadContent(request.TextToUpload, textDestinationPath, cancellationToken);
            await azureBlob.UploadContent(request.ImageToUpload, imageDestinationPath, cancellationToken);
            
            return await Task.FromResult(newArticle.Id);
        }
    }
}