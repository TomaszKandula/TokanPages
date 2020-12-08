using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TokanPages.BackEnd.Settings;
using TokanPages.BackEnd.Database;
using TokanPages.BackEnd.Database.Model;

namespace BackEnd.IntegrationTests
{

    public class ServiceTest_CosmosDb
    {

        private readonly CosmosDb FCosmosDb;

        public ServiceTest_CosmosDb()
        {

            var Configuration = new ConfigurationBuilder()
                .AddUserSecrets<ServiceTest_CosmosDb>()
                .Build();

            FCosmosDb = Configuration.GetSection("CosmosDb").Get<CosmosDb>();

        }

        [Fact]
        public async Task Should_GetOneItem()
        {

            // Arrange
            var LCosmosDbService = new CosmosDbService(FCosmosDb);
            LCosmosDbService.InitContainer<Articles>();

            // Act
            var LResult = await LCosmosDbService.GetItems<Articles>("select top 1 * from c");

            // Assert
            LResult.Should().NotBeNull();
            LResult.Should().HaveCount(1);

        }

        [Fact]
        public async Task Should_GetManyItems()
        {

            // Arrange
            var LCosmosDbService = new CosmosDbService(FCosmosDb);
            LCosmosDbService.InitContainer<Articles>();

            // Act
            var LResult = await LCosmosDbService.GetItems<Articles>("select * from c");

            // Assert
            LResult.Should().NotBeNull();
            LResult.Should().HaveCountGreaterThan(0);

        }

    }

}
