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

        public async Task<Article> GetItemAsync(string AId)
        {
            try
            {
                ItemResponse<Article> LResponse = await FContainer.ReadItemAsync<Article>(AId, new PartitionKey(AId));
                return LResponse.Resource;
            }
            catch (CosmosException Exception) when (Exception.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Article>> GetItemsAsync(string AQueryString)
        {
            var LQuery = FContainer.GetItemQueryIterator<Article>(new QueryDefinition(AQueryString));
            var LResults = new List<Article>();

            while (LQuery.HasMoreResults)
            {
                var LResponse = await LQuery.ReadNextAsync();
                LResults.AddRange(LResponse.ToList());
            }

            return LResults;
        }

        public async Task AddItemAsync(Article AItem)
        {
            await FContainer.CreateItemAsync(AItem, new PartitionKey(AItem.Id));
        }

        public async Task UpdateItemAsync(string AId, Article AItem)
        {
            await FContainer.UpsertItemAsync(AItem, new PartitionKey(AId));
        }

        public async Task DeleteItemAsync(string AId)
        {
            await FContainer.DeleteItemAsync<Article>(AId, new PartitionKey(AId));
        }

    }

}
