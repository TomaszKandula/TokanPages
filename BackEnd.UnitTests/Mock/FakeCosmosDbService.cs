using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Database;
using TokanPages.BackEnd.Database.Model;

namespace BackEnd.UnitTests.Database
{
 
    public class FakeCosmosDbService : CosmosDbService
    {

        public FakeCosmosDbService() 
        {        
        }

        public List<Article> DummyArticles { get; set; }

        public override async Task<Article> GetItemAsync(string AId) 
        {

            return await Task.Run(() => 
            {

                if (DummyArticles != null)
                {

                    var LDummies = DummyArticles.Where(R => R.Id == AId).ToList();
                    if (!LDummies.Any()) return null;

                    var LReturnedArticle = new Article
                    {
                        Id     = LDummies.Select(R => R.Id).FirstOrDefault(),
                        Title  = LDummies.Select(R => R.Title).FirstOrDefault(),
                        Desc   = LDummies.Select(R => R.Desc).FirstOrDefault(),
                        Status = LDummies.Select(R => R.Status).FirstOrDefault(),
                        Likes  = LDummies.Select(R => R.Likes).FirstOrDefault()
                    };

                    return LReturnedArticle;

                }
                else
                {
                    return null;
                }

            });

        }

        public override async Task<IEnumerable<Article>> GetItemsAsync(string AQueryString) 
        {

            return await Task.Run(() => 
            {

                if (DummyArticles != null 
                    && !string.IsNullOrEmpty(AQueryString) 
                    && !string.IsNullOrWhiteSpace(AQueryString))
                {
                    return DummyArticles.Select(R => R);
                }
                else
                {
                    return null;
                }

            });                

        }

        public override async Task<HttpStatusCode> AddItemAsync(Article AItem)
        {

            return await Task.Run(() => 
            {

                if (AItem != null)
                {
                    DummyArticles.Add(AItem);
                    return HttpStatusCode.OK;
                }
                else 
                {
                    return HttpStatusCode.BadRequest;
                }

            });

        }

        public override async Task<HttpStatusCode> UpdateItemAsync(string AId, Article AItem)
        {

            return await Task.Run(() =>
            {

                if (DummyArticles != null && DummyArticles.Any())
                {

                    if (AItem != null)
                    {

                        foreach (var Item in DummyArticles) 
                        {

                            if (Item.Id == AId) 
                            {

                                Item.Id     = AItem.Id;
                                Item.Title  = AItem.Title;
                                Item.Desc   = AItem.Desc;
                                Item.Status = AItem.Status;
                                Item.Likes  = AItem.Likes;

                                return HttpStatusCode.OK;

                            }

                        }

                        return HttpStatusCode.NotFound;

                    }
                    else 
                    {
                        return HttpStatusCode.BadRequest;
                    }

                }
                else 
                {
                    return HttpStatusCode.NotFound;
                }

            });

        }

        public override async Task<HttpStatusCode> DeleteItemAsync(string AId)
        {

            return await Task.Run(() =>
            {

                if (DummyArticles != null && DummyArticles.Any()) 
                {

                    var ObjectToRemove = DummyArticles.Select(R => R).Where(R => R.Id == AId).ToList();
                    if (!ObjectToRemove.Any()) return HttpStatusCode.BadRequest;

                    DummyArticles.Remove(ObjectToRemove[0]);
                    return HttpStatusCode.OK;

                }
                else
                {
                    return HttpStatusCode.NotFound;
                }

            });

        }

    }

}
