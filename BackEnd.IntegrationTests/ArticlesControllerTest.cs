using Xunit;
using FluentAssertions;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using BackEnd.IntegrationTests.Configuration;
using TokanPages;
using TokanPages.BackEnd.Controllers.Articles.Model;
using TokanPages.BackEnd.Controllers.Articles.Model.Responses;

namespace BackEnd.IntegrationTests
{

    public class ArticlesControllerTest : IClassFixture<TestFixture<Startup>>
    {

        private readonly HttpClient FHttpClient;

        public ArticlesControllerTest(TestFixture<Startup> ACustomFixture)
        {
            FHttpClient = ACustomFixture.FClient;
        }

        [Fact]
        public async Task GetItemsAsync_Test()
        {

            // Arrange
            var LRequest = "/api/v1/articles/";

            // Act
            var LResponse = await FHttpClient.GetAsync(LRequest);

            // Assert
            var LStringResponse = await LResponse.Content.ReadAsStringAsync();
            var LReturnArticles = JsonConvert.DeserializeObject<ReturnArticles>(LStringResponse);

            LReturnArticles.Error.ErrorDesc.Should().Be("n/a");
            LReturnArticles.Articles.Should().HaveCount(6);

        }

        [Theory]
        [InlineData("80cc8b7b-56f6-4e9d-8e17-0dc010b892d2")]
        public async Task GetItemAsync_Test(string Id) 
        {

            // Arrange
            var LRequest = $"/api/v1/articles/{Id}/";

            // Act
            var LResponse = await FHttpClient.GetAsync(LRequest);

            // Assert
            var LStringResponse = await LResponse.Content.ReadAsStringAsync();
            var LReturnArticle = JsonConvert.DeserializeObject<ReturnArticle>(LStringResponse);

            LReturnArticle.Error.ErrorDesc.Should().Be("n/a");
            LReturnArticle.Article.Title.Should().Be("abc");

        }

        [Fact]
        public async Task AddItemAsync_Test() 
        {

            // Arrange
            var LRequest = "/api/v1/articles/";

            var LNewGuid = Guid.NewGuid();
            var LPayLoad = new ArticleRequest 
            {
                Title  = "Integration test",
                Desc   = $"Test run: {LNewGuid}",
                Status = "draft",
                Likes  = 0
            };

            // Act
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, LRequest);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            var LResponse = await FHttpClient.SendAsync(LNewRequest);
            var LContent = await LResponse.Content.ReadAsStringAsync();

            // Assert
            LResponse.StatusCode.Should().Be(200);
            var LDeserialized = JsonConvert.DeserializeObject<ArticleAdded>(LContent);
            LDeserialized.NewUid.Should().NotBeNullOrEmpty();

        }

        [Fact]
        public async Task ChangeItemAsync_Test() 
        {

            // Arrange
            var LRequest = "/api/v1/articles/";

            var LNewGuid = Guid.NewGuid();
            var LPayLoad = new ArticleRequest
            {
                Id     = "e8722b93-5f99-4fec-996b-3a3bd401079f",
                Title  = "Integration test",
                Desc   = $"Test run: {LNewGuid}",
                Status = "draft",
                Likes  = 0
            };

            // Act
            var LNewRequest = new HttpRequestMessage(HttpMethod.Patch, LRequest);
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            var LResponse = await FHttpClient.SendAsync(LNewRequest);
            var LContent = await LResponse.Content.ReadAsStringAsync();

            // Assert
            LResponse.StatusCode.Should().Be(200);
            var LDeserialized = JsonConvert.DeserializeObject<ArticleUpdated>(LContent);
            LDeserialized.IsSucceeded.Should().BeTrue();

        }

        [Theory]
        [InlineData("ffebce1e-c4e5-47c8-9f3a-8e45c23a6755")]
        public async Task RemoveItemAsync_test(string Id) 
        {

            // Arrange
            var LRequest = $"/api/v1/articles/{Id}/";

            // Act
            var LResponse = await FHttpClient.DeleteAsync(LRequest);

            // Assert
            var LStringResponse = await LResponse.Content.ReadAsStringAsync();
            var LArticleDeleted = JsonConvert.DeserializeObject<ArticleDeleted>(LStringResponse);

            LArticleDeleted.Error.ErrorDesc.Should().Be("n/a");
            LArticleDeleted.IsSucceeded.Should().BeTrue();

        }

    }

}
