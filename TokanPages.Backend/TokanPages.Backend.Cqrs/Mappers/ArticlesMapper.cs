namespace TokanPages.Backend.Cqrs.Mappers
{
    using System.Diagnostics.CodeAnalysis;
    using Shared.Dto.Articles;
    using Handlers.Commands.Articles;

    [ExcludeFromCodeCoverage]
    public static class ArticlesMapper
    {
        public static AddArticleCommand MapToAddArticleCommand(AddArticleDto AModel) => new()
        {
            Title = AModel.Title,
            Description = AModel.Description,
            TextToUpload = AModel.TextToUpload,
            ImageToUpload = AModel.ImageToUpload
        };
        
        public static UpdateArticleContentCommand MapToUpdateArticleCommand(UpdateArticleContentDto AModel) => new()
        {
            Id = AModel.Id,
            Title = AModel.Title,
            Description = AModel.Description,
            TextToUpload = AModel.TextToUpload,
            ImageToUpload = AModel.ImageToUpload
        };

        public static UpdateArticleCountCommand MapToUpdateArticleCommand(UpdateArticleCountDto AModel) => new()
        {
            Id = AModel.Id
        };
        
        public static UpdateArticleLikesCommand MapToUpdateArticleCommand(UpdateArticleLikesDto AModel) => new() 
        {
            Id = AModel.Id,
            AddToLikes = AModel.AddToLikes 
        };

        public static UpdateArticleVisibilityCommand MapToUpdateArticleCommand(UpdateArticleVisibilityDto AModel) => new()
        {
            Id = AModel.Id, 
            IsPublished = AModel.IsPublished
        };
        
        public static RemoveArticleCommand MapToRemoveArticleCommand(RemoveArticleDto AModel) => new() 
        { 
            Id = AModel.Id
        };
    }
}