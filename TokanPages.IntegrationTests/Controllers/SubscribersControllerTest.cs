using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.DataProviders;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.Dto.Subscribers;
using TokanPages.Backend.Database.Initializer.Data;
using TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers;

namespace TokanPages.IntegrationTests.Controllers
{
    public class SubscribersControllerTest : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public SubscribersControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory)
            => FWebAppFactory = AWebAppFactory;
        
        [Fact]
        public async Task GivenAllFieldsAreCorrect_WhenAddSubscriber_ShouldReturnNewGuid() 
        {
            // Arrange
            const string REQUEST = "/api/v1/subscribers/addsubscriber/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);
            var LPayLoad = new AddSubscriberDto { Email = StringProvider.GetRandomEmail() };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.IsGuid().Should().BeTrue();
        }
        
        [Fact]
        public async Task WhenGetAllUsers_ShouldReturnCollection()
        {
            // Arrange
            const string REQUEST = "/api/v1/subscribers/getallsubscribers/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(REQUEST);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert
                .DeserializeObject<IEnumerable<GetAllSubscribersQueryResult>>(LContent)
                .ToList();
            
            LDeserialized.Should().NotBeNullOrEmpty();
            LDeserialized.Should().HaveCountGreaterThan(0);
        }
        
        [Fact]
        public async Task GivenCorrectId_WhenGetSubscriber_ShouldReturnEntityAsJsonObject() 
        {
            // Arrange
            var LTestUserId = Subscribers1.FId;
            var LRequest = $"/api/v1/subscribers/getsubscriber/{LTestUserId}/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<GetSubscriberQueryResult>(LContent);
            LDeserialized.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenGetSubscriber_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            const string REQUEST = "/api/v1/subscribers/getsubscriber/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(REQUEST);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
        }
        
        [Fact]
        public async Task GivenIncorrectId_WhenRemoveSubscriber_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            const string REQUEST = "/api/v1/subscribers/removesubscriber/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new RemoveSubscriberDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2")
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
        }

        [Fact]
        public async Task GivenIncorrectId_WhenUpdateSubscriber_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            const string REQUEST = "/api/v1/subscribers/updatesubscriber/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new UpdateSubscriberDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2"),
                Email = StringProvider.GetRandomEmail(),
                Count = null,
                IsActivated = null
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
        }
    }
}