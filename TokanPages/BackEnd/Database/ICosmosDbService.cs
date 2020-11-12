using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Database.Model;

namespace TokanPages.BackEnd.Database
{

    public interface ICosmosDbService
    {
        Task<Article> GetItemAsync(string AId);
        Task<IEnumerable<Article>> GetItemsAsync(string AQueryString);
        Task<HttpStatusCode> AddItemAsync(Article AItem);
        Task<HttpStatusCode> UpdateItemAsync(string AId, Article AItem);
        Task<HttpStatusCode> DeleteItemAsync(string AId);
    }

}
