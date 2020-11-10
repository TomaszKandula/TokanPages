using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using TokanPages.BackEnd.Database.Model;

namespace TokanPages.BackEnd.Database
{

    public class CosmosDbService : ICosmosDbService
    {

        private readonly Container FContainer;

        public CosmosDbService(CosmosClient ACosmosClient, string ADatabaseName, string AContainerName)
        {
            FContainer = ACosmosClient.GetContainer(ADatabaseName, AContainerName);
        }

        public async Task<Item> GetItemAsync(string AUid)
        {
            try
            {
                ItemResponse<Item> LResponse = await FContainer.ReadItemAsync<Item>(AUid, new PartitionKey(AUid));
                return LResponse.Resource;
            }
            catch (CosmosException Exception) when (Exception.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(string AQueryString)
        {
            var LQuery = FContainer.GetItemQueryIterator<Item>(new QueryDefinition(AQueryString));
            var LResults = new List<Item>();

            while (LQuery.HasMoreResults)
            {
                var LResponse = await LQuery.ReadNextAsync();
                LResults.AddRange(LResponse.ToList());
            }

            return LResults;
        }

        public async Task AddItemAsync(Item AItem)
        {
            await FContainer.CreateItemAsync(AItem, new PartitionKey(AItem.Uid));
        }

        public async Task UpdateItemAsync(string AUid, Item AItem)
        {
            await FContainer.UpsertItemAsync(AItem, new PartitionKey(AUid));
        }

        public async Task DeleteItemAsync(string AUid)
        {
            await FContainer.DeleteItemAsync<Item>(AUid, new PartitionKey(AUid));
        }

    }

}
