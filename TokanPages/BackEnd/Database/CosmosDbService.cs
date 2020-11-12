using System.Net;
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

        public CosmosDbService()
        {
        }

        public virtual async Task<Article> GetItem(string AId)
        {
            try
            {
                ItemResponse<Article> LResponse = await FContainer.ReadItemAsync<Article>(AId, new PartitionKey(AId));
                return LResponse.Resource;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
        }

        public virtual async Task<IEnumerable<Article>> GetItems(string AQueryString)
        {

            try 
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
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }          
            
        }

        public virtual async Task<HttpStatusCode> AddItem(Article AItem)
        {

            try 
            {
                var Response = await FContainer.CreateItemAsync(AItem, new PartitionKey(AItem.Id));
                return Response.StatusCode;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.Created)
            {
                return LException.StatusCode;
            }

        }

        public virtual async Task<HttpStatusCode> UpdateItem(string AId, Article AItem)
        {

            try
            {
                var Response = await FContainer.UpsertItemAsync(AItem, new PartitionKey(AId));
                return Response.StatusCode;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.OK)
            {
                return LException.StatusCode;
            }

        }

        public virtual async Task<HttpStatusCode> DeleteItem(string AId)
        {

            try
            {
                var Response = await FContainer.DeleteItemAsync<Article>(AId, new PartitionKey(AId));
                return Response.StatusCode;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.NoContent)
            {
                return LException.StatusCode;
            }

        }

    }

}
