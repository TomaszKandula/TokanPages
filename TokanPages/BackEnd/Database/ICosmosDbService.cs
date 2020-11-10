using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.BackEnd.Database.Model;

namespace TokanPages.BackEnd.Database
{

    public interface ICosmosDbService
    {

        Task<IEnumerable<Item>> GetItemsAsync(string query);

        Task<Item> GetItemAsync(string id);

        Task AddItemAsync(Item item);

        Task UpdateItemAsync(string id, Item item);

        Task DeleteItemAsync(string id);

    }

}
