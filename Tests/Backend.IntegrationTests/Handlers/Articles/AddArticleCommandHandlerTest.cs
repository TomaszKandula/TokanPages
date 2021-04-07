using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Backend.TestData;
using TokanPages;
using TokanPages.Backend.Shared.Dto.Articles;

namespace Backend.IntegrationTests.Handlers.Articles
{
    public class AddArticleCommandHandlerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;

        public AddArticleCommandHandlerTest(CustomWebApplicationFactory<Startup> AWebAppFactory)
        {
            FWebAppFactory = AWebAppFactory;
        }

        [Fact]
        public async Task AddArticle_WhenAllFieldsAreCorrect_ShouldReturnNewGuid()
        {
            // Arrange
            const string REQUEST = "/api/v1/articles/addarticle/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new AddArticleDto
            {
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                TextToUpload = DataProvider.GetRandomString(150),
                ImageToUpload = DataProvider.Base64Encode(DataProvider.GetRandomString(255))
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            GuidTest.Check(LContent).Should().BeTrue();
        }
    }
}
