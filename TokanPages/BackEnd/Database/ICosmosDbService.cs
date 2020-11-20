using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TokanPages.BackEnd.Database
{

    public interface ICosmosDbService
    {
        void InitContainer<T>();
        Task<T> GetItem<T>(string AId) where T : class;
        Task<IEnumerable<T>> GetItems<T>(string AQueryString) where T : class;
        Task<HttpStatusCode> AddItem<T>(string AId, T AItem);
        Task<HttpStatusCode> UpdateItem<T>(string AId, T AItem);
        Task<HttpStatusCode> DeleteItem<T>(string AId);
    }

}
