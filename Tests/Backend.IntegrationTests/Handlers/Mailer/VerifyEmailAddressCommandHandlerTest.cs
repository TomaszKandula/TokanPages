using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using TokanPages;
using TokanPages.Backend.Shared.Dto.Mailer;
using TokanPages.Backend.Cqrs.Handlers.Commands.Mailer;

namespace Backend.IntegrationTests.Handlers.Mailer
{

    public class VerifyEmailAddressCommandHandlerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {

        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;

        public VerifyEmailAddressCommandHandlerTest(CustomWebApplicationFactory<Startup> AWebAppFactory)
        {
            FWebAppFactory = AWebAppFactory;
        }

        [Theory]
        [InlineData("john@gmail.com")]
        public async Task VerifyEmailAddress_WhenEmailIsCorrect_ShouldReturnResultsAsJsonObject(string Email)
        {

            // Arrange
            var LRequest = $"/api/v1/mailer/verifyemailaddress/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);
            var LPayLoad = new VerifyEmailAddressDto { Email = Email };
            var LHttpClient = FWebAppFactory.CreateClient();

            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<VerifyEmailAddressCommandResult>(LContent);
            LDeserialized.IsFormatCorrect.Should().BeTrue();
            LDeserialized.IsDomainCorrect.Should().BeTrue();

        }

    }

}
