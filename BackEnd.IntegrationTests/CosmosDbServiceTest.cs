using Xunit;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using TokanPages.BackEnd.Settings;
using TokanPages.BackEnd.Database;

namespace BackEnd.IntegrationTests
{

    public class CosmosDbServiceTest
    {

        private readonly CosmosDb FCosmosDb;

        public CosmosDbServiceTest() 
        {

            var Configuration = new ConfigurationBuilder()
                .AddUserSecrets<CosmosDbServiceTest>()
                .Build();

            FCosmosDb = Configuration.GetSection("CosmosDb").Get<CosmosDb>();

        }

        [Fact]
        public async Task StartCosmosClient_Test() 
        {

            // Arrange
            var LDatabaseName = FCosmosDb.DatabaseName;
            var LContainerName = "TestContainer";
            var LAccount = FCosmosDb.Account;
            var LKey = FCosmosDb.Key;

            var LClient = new CosmosClient(LAccount, LKey, new CosmosClientOptions()
            {
                SerializerOptions = new CosmosSerializationOptions()
                {
                    PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
                }
            });

            var LCosmosDbService = new CosmosDbService(LClient, LDatabaseName, LContainerName);

            // Act
            var LDatabase = await LClient.CreateDatabaseIfNotExistsAsync(LDatabaseName);
            var LResult = await LDatabase.Database.CreateContainerIfNotExistsAsync(LContainerName, "/id");

            // Assert
            LCosmosDbService.Should().NotBeNull();
            LDatabase.StatusCode.Should().Be(HttpStatusCode.OK);
            LResult.StatusCode.Should().Be(HttpStatusCode.OK);

        }

    }

}
