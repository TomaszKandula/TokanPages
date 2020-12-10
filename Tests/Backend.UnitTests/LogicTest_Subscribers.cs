using Xunit;
using FluentAssertions;
using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using Backend.UnitTests.CosmosDbEmulator;
using TokanPages.Logic.Subscribers;
using TokanPages.Controllers.Subscribers.Model;
using SubscribersModel = TokanPages.Backend.Domain.Entities.Subscribers;
using TokanPages.Backend.Database;
using TokanPages.Backend.Database.Settings;

namespace Backend.UnitTests
{

    public class LogicTest_Subscribers
    {

        [Fact]
        public async Task Should_GetAllSubscribers() 
        {

            // Arrange
            var LCosmosDbSettings = new CosmosDbSettings()
            {
                DatabaseName = CosmosDbConfig.DatabaseName,
                Account = CosmosDbConfig.Account,
                Key = CosmosDbConfig.Key
            };
            var LCosmosDbService = new CosmosDbService(LCosmosDbSettings);
            LCosmosDbService.InitContainer<SubscribersModel>();

            // Act
            var LSubscribers = new Subscribers(LCosmosDbService);
            var LResult = await LSubscribers.GetAllSubscribers();

            // Assert
            LResult.Should().NotBeNull();
            LResult.Count().Should().BeGreaterThan(0);

        }

        [Fact]
        public async Task Should_GetSingleSubscriber()
        {

            // Arrange
            var LCosmosDbSettings = new CosmosDbSettings()
            {
                DatabaseName = CosmosDbConfig.DatabaseName,
                Account = CosmosDbConfig.Account,
                Key = CosmosDbConfig.Key
            };
            var LCosmosDbService = new CosmosDbService(LCosmosDbSettings);
            LCosmosDbService.InitContainer<SubscribersModel>();

            // Act
            var LSubscribers = new Subscribers(LCosmosDbService);
            var LResult = await LSubscribers.GetSingleSubscriber(Guid.Parse("5b2fc2f7-36ad-49d8-9b54-63bd6d8115cd"));

            // Assert
            LResult.Should().NotBeNull();
            LResult.Email.Should().Be("tokan@dfds.com");

        }

        [Fact]
        public async Task Should_AddNewSubscriber()
        {

            // Arrange
            var LCosmosDbSettings = new CosmosDbSettings()
            {
                DatabaseName = CosmosDbConfig.DatabaseName,
                Account = CosmosDbConfig.Account,
                Key = CosmosDbConfig.Key
            };
            var LCosmosDbService = new CosmosDbService(LCosmosDbSettings);
            LCosmosDbService.InitContainer<SubscribersModel>();

            var LPayLoad = new SubscriberRequest 
            { 
                Id     = Guid.Empty,
                Email  = "tokan@dfds.com",
                Status = "active",
                Count  = 0
            };

            // Act
            var LSubscribers = new Subscribers(LCosmosDbService);
            var LResult = await LSubscribers.AddNewSubscriber(LPayLoad);

            // Assert
            LResult.Should().NotBeNull();
            Guid.TryParse(LResult.NewId.ToString(), out _).Should().BeTrue();

        }

        [Fact]
        public async Task Should_UpdateSubscriber()
        {

            // Arrange
            var LCosmosDbSettings = new CosmosDbSettings()
            {
                DatabaseName = CosmosDbConfig.DatabaseName,
                Account = CosmosDbConfig.Account,
                Key = CosmosDbConfig.Key
            };
            var LCosmosDbService = new CosmosDbService(LCosmosDbSettings);
            LCosmosDbService.InitContainer<SubscribersModel>();

            var LPayLoad = new SubscriberRequest 
            { 
                Id     = Guid.Parse("5b2fc2f7-36ad-49d8-9b54-63bd6d8115cd"),
                Email  = "tokan@dfds.com",
                Status = "active",
                Count  = 100
            };

            // Act
            var LSubscribers = new Subscribers(LCosmosDbService);
            var LResult = await LSubscribers.UpdateSubscriber(LPayLoad);

            // Assert
            LResult.Should().NotBeNull();
            LResult.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public async Task Should_FailToDeleteSubscriber()
        {

            // Arrange
            var LCosmosDbSettings = new CosmosDbSettings()
            {
                DatabaseName = CosmosDbConfig.DatabaseName,
                Account = CosmosDbConfig.Account,
                Key = CosmosDbConfig.Key
            };
            var LCosmosDbService = new CosmosDbService(LCosmosDbSettings);
            LCosmosDbService.InitContainer<SubscribersModel>();

            // Act
            var LSubscribers = new Subscribers(LCosmosDbService);
            var LResult = await LSubscribers.DeleteSubscriber(Guid.Parse("5b2fc2f7-36ad-49d8-9b54-63bd6d8115cc"));

            // Assert
            LResult.Should().NotBeNull();
            LResult.Should().Be(HttpStatusCode.NotFound);

        }

    }

}
