using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TokanPages.BackEnd.Database
{

    public abstract class CosmosDbObject
    {
        public abstract void InitContainer<T>();
        public abstract Task<HttpStatusCode> CreateDatabase(string ADatabaseName);
        public abstract Task<HttpStatusCode> CreateContainer(string ADatabaseName, string AContainerName, string AId);
        public abstract Task<T> GetItem<T>(string AId) where T : class;
        public abstract Task<IEnumerable<T>> GetItems<T>(string AQueryString) where T : class;
        public abstract Task<HttpStatusCode> AddItem<T>(string AId, T AItem);
        public abstract Task<HttpStatusCode> UpdateItem<T>(string AId, T AItem);
        public abstract Task<HttpStatusCode> DeleteItem<T>(string AId);
    }

}
