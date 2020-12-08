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
        public async Task Should_GetAllArticles()
        {

            // Arrange
            var LRequest = "/api/v1/articles/";

            // Act
            var LResponse = await FHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<ReturnArticles>(LContent);
            LDeserialized.Error.ErrorDesc.Should().Be("n/a");

        }

        [Theory]
        [InlineData("a8db7e28-2d47-463c-9c38-c17706056f72")]
        public async Task Should_GetSingleArticle(string Id) 
        {

            // Arrange
            var LRequest = $"/api/v1/articles/{Id}/";

            // Act
            var LResponse = await FHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<ReturnArticle>(LContent);
            LDeserialized.Error.ErrorDesc.Should().Be("n/a");
            LDeserialized.Article.Title.Should().Be("abc");

        }

        [Fact]
        public async Task Should_AddNewArticle() 
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

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<ArticleAdded>(LContent);
            LDeserialized.NewUid.Should().NotBeNullOrEmpty();

            Guid.TryParse(LDeserialized.NewUid, out _).Should().BeTrue();

        }

        [Fact]
        public async Task Should_UpdateArticle() 
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

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<ArticleUpdated>(LContent);
            LDeserialized.IsSucceeded.Should().BeTrue();

        }

        [Theory]
        [InlineData("invalid-id")]
        public async Task Should_FailToDeleteArticle(string Id) 
        {

            // Arrange
            var LRequest = $"/api/v1/articles/{Id}/";

            // Act
            var LResponse = await FHttpClient.DeleteAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<ArticleDeleted>(LContent);
            LDeserialized.Error.ErrorDesc.Should().NotBe("n/a");
            LDeserialized.IsSucceeded.Should().BeFalse();

        }

    }

}
