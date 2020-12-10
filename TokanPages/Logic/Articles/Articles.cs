using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Controllers.Articles.Model;

namespace TokanPages.BackEnd.Logic.Articles
{

    public class Articles : IArticles
    {

        private readonly ICosmosDbService FCosmosDbService;

        public Articles(ICosmosDbService ACosmosDbService)
        {
            FCosmosDbService = ACosmosDbService;
            FCosmosDbService.InitContainer<ArticlesModel>();
        }

        public async Task<List<ArticleItem>> GetAllArticles()
        {

            var LItems = await FCosmosDbService.GetItems<ArticlesModel>($"select * from {typeof(ArticlesModel).Name}");
            if (LItems == null || !LItems.Any()) return null;

            var LResult = new List<ArticleItem>();
            foreach (var LItem in LItems)
            {

                var ArticleItem = new ArticleItem
                {
                    Id        = LItem.Id,
                    Title     = LItem.Title,
                    Desc      = LItem.Desc,
                    Status    = LItem.Status,
                    Likes     = LItem.Likes,
                    ReadCount = LItem.ReadCount
                };

                LResult.Add(ArticleItem);

            }

            return LResult;

        }

        public async Task<ArticleItem> GetSingleArticle(Guid Id)
        {

            var LItem = await FCosmosDbService.GetItem<ArticlesModel>(Id);
            if (LItem == null) return null;

            return new ArticleItem 
            {
                Id        = LItem.Id,
                Title     = LItem.Title,
                Desc      = LItem.Desc,
                Status    = LItem.Status,
                Likes     = LItem.Likes,
                ReadCount = LItem.ReadCount
            };

        }

        public async Task<Guid> AddNewArticle(ArticleRequest PayLoad)
        {

            var NewId = Guid.NewGuid();
            var InsertNew = new ArticlesModel
            {
                Id        = NewId,
                Title     = PayLoad.Title,
                Desc      = PayLoad.Desc,
                Status    = PayLoad.Status,
                Likes     = 0,
                ReadCount = 0
            };

            if (await FCosmosDbService.AddItem<ArticlesModel>(NewId, InsertNew) == HttpStatusCode.Created)
            {
                return NewId;
            }
            else 
            {
                return Guid.Empty;
            }

        }

        public async Task<HttpStatusCode> UpdateArticle(ArticleRequest PayLoad) 
        {

            var LResult = await FCosmosDbService.IsItemExists<ArticlesModel>(PayLoad.Id);
            if (LResult != HttpStatusCode.OK) return LResult;

            var UpdatedArticle = new ArticlesModel
            { 
                Id        = PayLoad.Id,
                Title     = PayLoad.Title,
                Desc      = PayLoad.Desc,
                Status    = PayLoad.Status,
                Likes     = PayLoad.Likes,
                ReadCount = PayLoad.ReadCount
            };

            return await FCosmosDbService.UpdateItem<ArticlesModel>(PayLoad.Id, UpdatedArticle);

        }

        public async Task<HttpStatusCode> DeleteArticle(Guid Id) 
        {
            var LResult = await FCosmosDbService.IsItemExists<ArticlesModel>(Id);
            if (LResult != HttpStatusCode.OK) return LResult;
            return await FCosmosDbService.DeleteItem<ArticlesModel>(Id);
        }

    }

}
