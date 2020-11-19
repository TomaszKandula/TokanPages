using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Database;
using TokanPages.BackEnd.Database.Model;

namespace BackEnd.UnitTests.Mocks.CosmosDb
{
 
    public class FakeCosmosDbService : CosmosDbService
    {

        public FakeCosmosDbService() 
        {        
        }

        public List<Article> DummyArticles { get; set; }

        public override async Task<Article> GetItem(string AId) 
        {

            return await Task.Run(() => 
            {

                if (DummyArticles != null)
                {

                    var LDummies = DummyArticles
                        .Where(Item => Item.Id == AId)
                        .ToList();
                    
                    if (!LDummies.Any()) return null;

                    var LReturnedArticle = new Article
                    {
                        Id     = LDummies.Select(Item => Item.Id).FirstOrDefault(),
                        Title  = LDummies.Select(Item => Item.Title).FirstOrDefault(),
                        Desc   = LDummies.Select(Item => Item.Desc).FirstOrDefault(),
                        Status = LDummies.Select(Item => Item.Status).FirstOrDefault(),
                        Likes  = LDummies.Select(Item => Item.Likes).FirstOrDefault(),
                        ReadCount = LDummies.Select(Item => Item.ReadCount).FirstOrDefault()
                    };

                    return LReturnedArticle;

                }
                else
                {
                    return null;
                }

            });

        }

        public override async Task<IEnumerable<Article>> GetItems(string AQueryString) 
        {

            return await Task.Run(() => 
            {

                if (DummyArticles != null 
                    && !string.IsNullOrEmpty(AQueryString) 
                    && !string.IsNullOrWhiteSpace(AQueryString))
                {
                    return DummyArticles;
                }
                else
                {
                    return null;
                }

            });                

        }

        public override async Task<HttpStatusCode> AddItem(Article AItem)
        {

            return await Task.Run(() => 
            {

                if (AItem != null)
                {
                    DummyArticles.Add(AItem);
                    return HttpStatusCode.Created;
                }
                else 
                {
                    return HttpStatusCode.BadRequest;
                }

            });

        }

        public override async Task<HttpStatusCode> UpdateItem(string AId, Article AItem)
        {

            return await Task.Run(() =>
            {

                if (AId == null)
                    throw new ArgumentException("Argument cannot be null.", "AId");

                if (AItem == null)
                    throw new ArgumentException("Object cannot be null.", "AItem");

                if (DummyArticles == null && !DummyArticles.Any())
                    return HttpStatusCode.BadRequest;

                if (string.IsNullOrEmpty(AId) || string.IsNullOrWhiteSpace(AId)) 
                    return HttpStatusCode.BadRequest;

                foreach (var Item in DummyArticles)
                {

                    if (Item.Id == AId)
                    {

                        Item.Id     = AItem.Id;
                        Item.Title  = AItem.Title;
                        Item.Desc   = AItem.Desc;
                        Item.Status = AItem.Status;
                        Item.Likes  = AItem.Likes;
                        Item.ReadCount = AItem.ReadCount;
                        break;
                    }

                }

                return HttpStatusCode.OK;

            });

        }

        public override async Task<HttpStatusCode> DeleteItem(string AId)
        {

            return await Task.Run(() =>
            {

                if (AId == null)
                    throw new ArgumentException("Argument cannot be null.", "AId");

                if (DummyArticles != null && DummyArticles.Any()) 
                {

                    var ObjectToRemove = DummyArticles
                        .Select(Item => Item)
                        .Where(Item => Item.Id == AId).ToList();

                    if (!ObjectToRemove.Any()) return HttpStatusCode.NotFound;

                    DummyArticles.Remove(ObjectToRemove[0]);
                    return HttpStatusCode.NoContent;

                }
                else
                {
                    return HttpStatusCode.BadRequest;
                }

            });

        }

    }

}
