namespace TokanPages.Backend.Cqrs.Mappers
{
    using System.Diagnostics.CodeAnalysis;
    using Shared.Dto.Articles;
    using Handlers.Commands.Articles;

    [ExcludeFromCodeCoverage]
    public static class ArticlesMapper
    {
        public static AddArticleCommand MapToAddArticleCommand(AddArticleDto model) => new()
        {
            Title = model.Title,
            Description = model.Description,
            TextToUpload = model.TextToUpload,
            ImageToUpload = model.ImageToUpload
        };
        
        public static UpdateArticleContentCommand MapToUpdateArticleCommand(UpdateArticleContentDto model) => new()
        {
            Id = model.Id,
            Title = model.Title,
            Description = model.Description,
            TextToUpload = model.TextToUpload,
            ImageToUpload = model.ImageToUpload
        };

        public static UpdateArticleCountCommand MapToUpdateArticleCommand(UpdateArticleCountDto model) => new()
        {
            Id = model.Id
        };
        
        public static UpdateArticleLikesCommand MapToUpdateArticleCommand(UpdateArticleLikesDto model) => new() 
        {
            Id = model.Id,
            AddToLikes = model.AddToLikes 
        };

        public static UpdateArticleVisibilityCommand MapToUpdateArticleCommand(UpdateArticleVisibilityDto model) => new()
        {
            Id = model.Id, 
            IsPublished = model.IsPublished
        };
        
        public static RemoveArticleCommand MapToRemoveArticleCommand(RemoveArticleDto model) => new() 
        { 
            Id = model.Id
        };
    }
}