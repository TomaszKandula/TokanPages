﻿using System.Net;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Azure.Cosmos;
using TokanPages.BackEnd.Settings;

namespace TokanPages.BackEnd.Database
{

    public class CosmosDbService : ICosmosDbService
    {

        private Container FContainer { get; set; }
        private CosmosClient FCosmosClient { get; set; }
        private string FDatabaseName { get; set; }

        public CosmosDbService(CosmosDb ACosmosDb)
        {

            var FCosmosDb = ACosmosDb;
            var LAccount  = FCosmosDb.Account;
            var LKey      = FCosmosDb.Key;

            FDatabaseName  = FCosmosDb.DatabaseName;

            FCosmosClient = new CosmosClient(LAccount, LKey, new CosmosClientOptions()
            {
                SerializerOptions = new CosmosSerializationOptions()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            });

            FContainer = null;

        }

        public CosmosDbService()
        {
        }

        public void InitContainer<T>() 
        {
            // Names of CosmosDb container and item model used in the container must be the same
            var LModelName = typeof(T).Name;
            FContainer = FCosmosClient.GetContainer(FDatabaseName, LModelName);
        }

        public virtual async Task<T> GetItem<T>(string AId) where T : class
        {

            try
            {
                var LResponse = await FContainer.ReadItemAsync<T>(AId, new PartitionKey(AId));
                return LResponse.Resource;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }

        }

        public virtual async Task<IEnumerable<T>> GetItems<T>(string AQueryString) where T : class
        {

            try 
            {

                var LQuery = FContainer.GetItemQueryIterator<T>(new QueryDefinition(AQueryString));
                var LResults = new List<T>();

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

        public virtual async Task<HttpStatusCode> AddItem<T>(string AId, T AItem)
        {

            try 
            {
                var Response = await FContainer.CreateItemAsync<T>(AItem, new PartitionKey(AId));
                return Response.StatusCode;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.Created)
            {
                return LException.StatusCode;
            }

        }

        public virtual async Task<HttpStatusCode> UpdateItem<T>(string AId, T AItem)
        {

            try
            {
                var Response = await FContainer.UpsertItemAsync<T>(AItem, new PartitionKey(AId));
                return Response.StatusCode;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.OK)
            {
                return LException.StatusCode;
            }

        }

        public virtual async Task<HttpStatusCode> DeleteItem<T>(string AId)
        {

            try
            {
                var Response = await FContainer.DeleteItemAsync<T>(AId, new PartitionKey(AId));
                return Response.StatusCode;
            }
            catch (CosmosException LException) when (LException.StatusCode != HttpStatusCode.NoContent)
            {
                return LException.StatusCode;
            }

        }

    }

}
