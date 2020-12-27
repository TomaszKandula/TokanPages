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

    public class AddArticleCommandHandlerTest : IClassFixture<TestFixture<Startup>>
    {

        private readonly HttpClient FHttpClient;

        public AddArticleCommandHandlerTest(TestFixture<Startup> ACustomFixture)
        {
            FHttpClient = ACustomFixture.FClient;
        }

        [Fact]
        public async Task AddArticle_WhenAllFieldsAreCorrect_ShouldReturnNewGuid()
        {

            // Arrange
            var LRequest = $"/api/v1/articles/addarticle/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);

            var LPayLoad = new AddArticleDto
            {
                Title = DataProvider.GetRandomString(),
                Description = DataProvider.GetRandomString(),
                TextToUpload = DataProvider.GetRandomString(150),
                ImageToUpload = DataProvider.Base64Encode(DataProvider.GetRandomString(255))
            };

            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await FHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            GuidTest.Check(LContent).Should().BeTrue();

        }

    }

}
