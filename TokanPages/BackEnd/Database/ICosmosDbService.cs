using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Database.Model;

namespace TokanPages.BackEnd.Database
{

    public interface ICosmosDbService
    {
        Task<Article> GetItem(string AId);
        Task<IEnumerable<Article>> GetItems(string AQueryString);
        Task<HttpStatusCode> AddItem(Article AItem);
        Task<HttpStatusCode> UpdateItem(string AId, Article AItem);
        Task<HttpStatusCode> DeleteItem(string AId);
    }

}
