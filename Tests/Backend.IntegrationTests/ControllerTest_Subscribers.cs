using Xunit;
using FluentAssertions;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TokanPages;
using BackEnd.IntegrationTests.Configuration;
using TokanPages.Controllers.Subscribers.Model;
using TokanPages.Controllers.Subscribers.Model.Responses;

namespace BackEnd.IntegrationTests
{

    public class ControllerTest_Subscribers : IClassFixture<TestFixture<Startup>>
    {

        private readonly HttpClient FHttpClient;

        public ControllerTest_Subscribers(TestFixture<Startup> ACustomFixture)
        {
            FHttpClient = ACustomFixture.FClient;
        }

        [Fact]
        public async Task Should_GetAllSubscribers()
        {

            // Arrange
            var LRequest = "/api/v1/subscribers/";

            // Act
            var LResponse = await FHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<ReturnSubscribers>(LContent);
            LDeserialized.Subscribers.Should().NotBeNull();
            LDeserialized.Subscribers.Should().HaveCountGreaterThan(0);

        }

        [Theory]
        [InlineData("e738c997-178b-45e6-95c1-06a3d31ec829")]
        public async Task Should_GetSingleSubscriber(string Id)
        {

            // Arrange
            var LRequest = $"/api/v1/subscribers/{Id}/";

            // Act
            var LResponse = await FHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<ReturnSubscriber>(LContent);
            LDeserialized.Subscriber.Should().NotBeNull();
            LDeserialized.Subscriber.Id.Should().Be(Id);

        }

        [Fact]
        public async Task Should_AddNewSubscriber()
        {

            // Arrange
            var LRequest = "/api/v1/subscribers/";

            var LNewGuid = (Guid.NewGuid()).ToString();
            var LPayLoad = new SubscriberRequest
            {
                Id     = Guid.Empty,
                Email  = $"{LNewGuid[0..8]}@gmail.com",
                Status = "inactive",
                Count  = 0
            };

            // Act
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");
            var LResponse = await FHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            
            var LDeserialized = JsonConvert.DeserializeObject<SubscriberAdded>(LContent);
            Guid.TryParse(LDeserialized.NewUid.ToString(), out _).Should().BeTrue();
            LDeserialized.IsSucceeded.Should().BeTrue();

        }

        [Fact]
        public async Task Should_UpdateSubscriber()
        {

            // Arrange
            var LRequest = "/api/v1/subscribers/";

            var LPayLoad = new SubscriberRequest
            {
                Id     = Guid.Parse("e738c997-178b-45e6-95c1-06a3d31ec829"),
                Email  = "tokan@gmail.com",
                Status = "active",
                Count  = 100
            };

            // Act
            var LNewRequest = new HttpRequestMessage(HttpMethod.Patch, LRequest);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");
            var LResponse = await FHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<SubscriberUpdated>(LContent);
            LDeserialized.IsSucceeded.Should().BeTrue();

        }

        [Theory]
        [InlineData("352e356e-1865-412e-bade-a2016dfde55a")]
        public async Task Should_FailToDeleteSubscriber(string Id) 
        {

            // Arrange
            var LRequest = $"/api/v1/subscribers/{Id}/";

            // Act
            var LResponse = await FHttpClient.DeleteAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();
            
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<SubscriberDeleted>(LContent);
            LDeserialized.IsSucceeded.Should().BeFalse();

        }

    }

}
