using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;
using TokanPages.Backend.Shared.Dto.Articles;

namespace TokanPages.Backend.Cqrs.Mappers
{

    public static class ArticlesMapper
    {

        public static AddArticleCommand MapToAddArticleCommand(AddArticleDto AModel)
        {
            return new AddArticleCommand
            {
                Title = AModel.Title,
                Description = AModel.Description,
                TextToUpload = AModel.TextToUpload,
                ImageToUpload = AModel.ImageToUpload
            };
        }

        public static UpdateArticleCommand MapToUpateArticleCommand(UpdateArticleDto AModel)
        {
            return new UpdateArticleCommand
            {
                Id = AModel.Id,
                Title = AModel.Title,
                Description = AModel.Description,
                TextToUpload = AModel.TextToUpload,
                ImageToUpload = AModel.ImageToUpload,
                IsPublished = AModel.IsPublished,
                Likes = AModel.Likes,
                ReadCount = AModel.ReadCount
            };
        }

        public static RemoveArticleCommand MapToRemoveArticleCommand(RemoveArticleDto AModel) 
        {
            return new RemoveArticleCommand 
            { 
                Id = AModel.Id
            };
        }

    }

}
