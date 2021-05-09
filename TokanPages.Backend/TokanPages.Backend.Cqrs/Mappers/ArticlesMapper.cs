using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;
using TokanPages.Backend.Shared.Dto.Articles;

namespace TokanPages.Backend.Cqrs.Mappers
{
    public static class ArticlesMapper
    {
        public static AddArticleCommand MapToAddArticleCommand(AddArticleDto AModel)
        {
            return new ()
            {
                Title = AModel.Title,
                Description = AModel.Description,
                TextToUpload = AModel.TextToUpload,
                ImageToUpload = AModel.ImageToUpload
            };
        }

        public static UpdateArticleCommand MapToUpdateArticleCommand(UpdateArticleDto AModel)
        {
            return new ()
            {
                Id = AModel.Id,
                Title = AModel.Title,
                Description = AModel.Description,
                TextToUpload = AModel.TextToUpload,
                ImageToUpload = AModel.ImageToUpload,
                IsPublished = AModel.IsPublished,
                AddToLikes = AModel.AddToLikes,
                UpReadCount = AModel.UpReadCount
            };
        }

        public static RemoveArticleCommand MapToRemoveArticleCommand(RemoveArticleDto AModel) 
        {
            return new () 
            { 
                Id = AModel.Id
            };
        }
    }
}
