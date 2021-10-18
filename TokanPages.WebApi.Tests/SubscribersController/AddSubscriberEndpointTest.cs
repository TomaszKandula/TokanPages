namespace TokanPages.WebApi.Tests.SubscribersController
{
    using Xunit;
    using Newtonsoft.Json;
    using FluentAssertions;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend.Core.Extensions;
    using Backend.Shared.Dto.Subscribers;

    public partial class SubscribersControllerTest
    {
        [Fact]
        public async Task GivenAllFieldsAreCorrect_WhenAddSubscriber_ShouldReturnNewGuid() 
        {
            // Arrange
            var request = $"{ApiBaseUrl}/AddSubscriber/";
            var newRequest = new HttpRequestMessage(HttpMethod.Post, request);
            var payLoad = new AddSubscriberDto { Email = DataUtilityService.GetRandomEmail() };

            var httpClient = _webApplicationFactory.CreateClient();
            newRequest.Content = new StringContent(JsonConvert.SerializeObject(payLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var response = await httpClient.SendAsync(newRequest);
            await EnsureStatusCode(response, HttpStatusCode.OK);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();
            content.IsGuid().Should().BeTrue();
        }
    }
}