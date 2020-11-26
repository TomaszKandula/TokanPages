using Xunit;
using FluentAssertions;
using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using TokanPages.BackEnd.Settings;
using TokanPages.BackEnd.Database;
using BackEnd.UnitTests.CosmosDbEmulator;
using TokanPages.BackEnd.Logic.Subscribers;
using TokanPages.BackEnd.Controllers.Subscribers.Model;
using SubscribersModel = TokanPages.BackEnd.Database.Model.Subscribers;

namespace BackEnd.UnitTests
{

    public class LogicTest_Subscribers
    {

        [Fact]
        public async Task Should_GetAllSubscribers() 
        {

            // Arrange
            var LDbConfig = new CosmosDb()
            {
                DatabaseName = CosmosDbConfig.DatabaseName,
                Account = CosmosDbConfig.Account,
                Key = CosmosDbConfig.Key
            };
            var LCosmosService = new CosmosDbService(LDbConfig);
            LCosmosService.InitContainer<SubscribersModel>();

            // Act
            var LSubscribers = new Subscribers(LCosmosService);
            var LResult = await LSubscribers.GetAllSubscribers();

            // Assert
            LResult.Should().NotBeNull();
            LResult.Count().Should().BeGreaterThan(0);

        }

        [Fact]
        public async Task Should_GetSingleSubscriber()
        {

            // Arrange
            var LDbConfig = new CosmosDb()
            {
                DatabaseName = CosmosDbConfig.DatabaseName,
                Account = CosmosDbConfig.Account,
                Key = CosmosDbConfig.Key
            };
            var LCosmosService = new CosmosDbService(LDbConfig);
            LCosmosService.InitContainer<SubscribersModel>();

            // Act
            var LSubscribers = new Subscribers(LCosmosService);
            var LResult = await LSubscribers.GetSingleSubscriber("5b2fc2f7-36ad-49d8-9b54-63bd6d8115cd");

            // Assert
            LResult.Should().NotBeNull();
            LResult.Email.Should().Be("tokan@dfds.com");

        }

        [Fact]
        public async Task Should_AddNewSubscriber()
        {

            // Arrange
            var LDbConfig = new CosmosDb()
            {
                DatabaseName = CosmosDbConfig.DatabaseName,
                Account = CosmosDbConfig.Account,
                Key = CosmosDbConfig.Key
            };
            var LCosmosService = new CosmosDbService(LDbConfig);
            LCosmosService.InitContainer<SubscribersModel>();

            var LPayLoad = new SubscriberRequest 
            { 
                Id = null,
                Email = "tokan@dfds.com",
                Status = "active",
                Count = 0
            };

            // Act
            var LSubscribers = new Subscribers(LCosmosService);
            var LResult = await LSubscribers.AddNewSubscriber(LPayLoad);

            // Assert
            LResult.Should().NotBeNull();
            LResult.NewId.Should().NotBeNullOrEmpty();
            
            var IsGuid = Guid.TryParse(LResult.NewId, out _);
            IsGuid.Should().BeTrue();

        }

        [Fact]
        public async Task Should_UpdateSubscriber()
        {

            // Arrange
            var LDbConfig = new CosmosDb()
            {
                DatabaseName = CosmosDbConfig.DatabaseName,
                Account = CosmosDbConfig.Account,
                Key = CosmosDbConfig.Key
            };
            var LCosmosService = new CosmosDbService(LDbConfig);
            LCosmosService.InitContainer<SubscribersModel>();

            var LPayLoad = new SubscriberRequest 
            { 
                Id = "5b2fc2f7-36ad-49d8-9b54-63bd6d8115cd",
                Email = "tokan@dfds.com",
                Status = "active",
                Count = 100
            };

            // Act
            var LSubscribers = new Subscribers(LCosmosService);
            var LResult = await LSubscribers.UpdateSubscriber(LPayLoad);

            // Assert
            LResult.Should().NotBeNull();
            LResult.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public async Task Should_not_DeleteSubscriber()
        {

            // Arrange
            var LDbConfig = new CosmosDb()
            {
                DatabaseName = CosmosDbConfig.DatabaseName,
                Account = CosmosDbConfig.Account,
                Key = CosmosDbConfig.Key
            };
            var LCosmosService = new CosmosDbService(LDbConfig);
            LCosmosService.InitContainer<SubscribersModel>();

            // Act
            var LSubscribers = new Subscribers(LCosmosService);
            var LResult = await LSubscribers.DeleteSubscriber("invalid-id");

            // Assert
            LResult.Should().NotBeNull();
            LResult.Should().Be(HttpStatusCode.NotFound);

        }

    }

}
