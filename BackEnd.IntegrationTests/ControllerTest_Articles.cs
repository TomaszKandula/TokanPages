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

    public class ControllerTest_Articles : IClassFixture<TestFixture<Startup>>
    {

        private readonly HttpClient FHttpClient;

        public ControllerTest_Articles(TestFixture<Startup> ACustomFixture)
        {
            FHttpClient = ACustomFixture.FClient;
        }

        [Fact]
        public async Task Should_GetAllItems()
        {

            // Arrange
            var LRequest = "/api/v1/articles/";

            // Act
            var LResponse = await FHttpClient.GetAsync(LRequest);

            // Assert
            var LStringResponse = await LResponse.Content.ReadAsStringAsync();
            var LReturnArticles = JsonConvert.DeserializeObject<ReturnArticles>(LStringResponse);

            LReturnArticles.Error.ErrorDesc.Should().Be("n/a");

        }

        [Theory]
        [InlineData("a8db7e28-2d47-463c-9c38-c17706056f72")]
        public async Task Should_GetOneItem(string Id) 
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
        public async Task Should_AddNewItem() 
        {

            // Arrange
            var LRequest = "/api/v1/articles/";

            var LNewGuid = Guid.NewGuid();
            var LPayLoad = new ArticleRequest 
            {
                Title  = "Integration test",
                Desc   = $"Test run: {LNewGuid}",
                Status = "draft"
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
        public async Task Should_UpdateItem() 
        {

            // Arrange
            var LRequest = "/api/v1/articles/";

            var LNewGuid = Guid.NewGuid();
            var LPayLoad = new ArticleRequest
            {
                Id     = "ce4d995c-0fba-436b-93fe-ba81c5ba0745",
                Title  = "Integration test",
                Desc   = $"Test run: {LNewGuid}",
                Status = "draft",
                Likes  = 10,
                ReadCount = 100
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
        [InlineData("invalid")]
        public async Task Should_FailToDeleteItem(string Id) 
        {

            // Arrange
            var LRequest = $"/api/v1/articles/{Id}/";

            // Act
            var LResponse = await FHttpClient.DeleteAsync(LRequest);

            // Assert
            var LStringResponse = await LResponse.Content.ReadAsStringAsync();
            var LArticleDeleted = JsonConvert.DeserializeObject<ArticleDeleted>(LStringResponse);

            LArticleDeleted.Error.ErrorDesc.Should().NotBe("n/a");
            LArticleDeleted.IsSucceeded.Should().BeFalse();

        }

    }

}
