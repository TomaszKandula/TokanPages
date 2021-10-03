namespace TokanPages.WebApi.Tests.Controllers.SubscribersController
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Backend.Shared.Resources;
    using Backend.Shared.Dto.Subscribers;
    using Newtonsoft.Json;
    using FluentAssertions;
    using Xunit;

    public partial class SubscribersControllerTest
    {
        [Fact]
        public async Task GivenIncorrectIdAndNoJwt_WhenUpdateSubscriber_ShouldReturnUnauthorized()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/UpdateSubscriber/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new UpdateSubscriberDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2"),
                Email = DataUtilityService.GetRandomEmail(),
                Count = null,
                IsActivated = null
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.BadRequest);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS);
        }
    }
}