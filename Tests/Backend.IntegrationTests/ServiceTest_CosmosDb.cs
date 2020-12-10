using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Database;
using TokanPages.Backend.Database.Settings;
using TokanPages.Backend.Domain.Entities;

namespace Backend.IntegrationTests
{

    public class ServiceTest_CosmosDb
    {

        private readonly CosmosDbSettings FCosmosDbSettings;

        public ServiceTest_CosmosDb()
        {

            var Configuration = new ConfigurationBuilder()
                .AddUserSecrets<ServiceTest_CosmosDb>()
                .Build();

            FCosmosDbSettings = Configuration.GetSection("CosmosDb").Get<CosmosDbSettings>();

        }

        [Fact]
        public async Task Should_GetOneItem()
        {

            // Arrange
            var LCosmosDbService = new CosmosDbService(FCosmosDbSettings);
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
            var LCosmosDbService = new CosmosDbService(FCosmosDbSettings);
            LCosmosDbService.InitContainer<Articles>();

            // Act
            var LResult = await LCosmosDbService.GetItems<Articles>("select * from c");

            // Assert
            LResult.Should().NotBeNull();
            LResult.Should().HaveCountGreaterThan(0);

        }

    }

}
