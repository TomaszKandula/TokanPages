using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Database;
using TokanPages.BackEnd.Database.Model;
using TokanPages.BackEnd.Controllers.Articles.Model;

namespace TokanPages.BackEnd.Logic.Articles
{

    public class Articles : IArticles
    {

        private readonly ICosmosDbService FCosmosDbService;

        public Articles(ICosmosDbService ACosmosDbService)
        {
            FCosmosDbService = ACosmosDbService;
        }

        public async Task<List<ArticleItem>> GetAllArticles()
        {

            var LItems = await FCosmosDbService.GetItems("select * from c");
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

        public async Task<ArticleItem> GetSingleArticle(string Id)
        {

            var LItem = await FCosmosDbService.GetItem(Id);
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

        public async Task<string> AddNewArticle(ArticleRequest PayLoad)
        {

            var NewId = Guid.NewGuid().ToString();
            var InsertNew = new Article
            {
                Id        = NewId,
                Title     = PayLoad.Title,
                Desc      = PayLoad.Desc,
                Status    = PayLoad.Status,
                Likes     = 0,
                ReadCount = 0
            };

            if (await FCosmosDbService.AddItem(InsertNew) == HttpStatusCode.Created)
            {
                return NewId;
            }
            else 
            {
                return string.Empty;
            }

        }

        public async Task<HttpStatusCode> UpdateArticle(ArticleRequest PayLoad) 
        {

            var UpdatedArticle = new Article 
            { 
                Id        = PayLoad.Id,
                Title     = PayLoad.Title,
                Desc      = PayLoad.Desc,
                Status    = PayLoad.Status,
                Likes     = PayLoad.Likes,
                ReadCount = PayLoad.ReadCount
            };

            return await FCosmosDbService.UpdateItem(PayLoad.Id, UpdatedArticle);

        }

        public async Task<HttpStatusCode> DeleteArticle(string Id) 
        {
            return await FCosmosDbService.DeleteItem(Id);
        }

    }

}
