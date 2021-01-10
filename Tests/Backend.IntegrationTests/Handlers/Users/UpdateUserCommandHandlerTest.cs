using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.TestData;
using TokanPages;
using TokanPages.Backend.Shared.Dto.Users;
using TokanPages.Backend.Shared.Resources;

namespace Backend.IntegrationTests.Handlers.Users
{

    public class UpdateUserCommandHandlerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {

        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;

        public UpdateUserCommandHandlerTest(CustomWebApplicationFactory<Startup> AWebAppFactory)
        {
            FWebAppFactory = AWebAppFactory;
        }

        [Fact]
        public async Task UpdateUser_WhenIdIsIncorrect_ShouldReturnJsonObjectWithError() 
        {

            // Arrange
            var LRequest = $"/api/v1/users/updateuser/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new UpdateUserDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2"),
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                IsActivated = true
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.USER_DOES_NOT_EXISTS);

        }

    }

}
