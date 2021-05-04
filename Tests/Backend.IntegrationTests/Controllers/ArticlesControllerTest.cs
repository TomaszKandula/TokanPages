using Xunit;
using Newtonsoft.Json;
using FluentAssertions;
using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Database.Dummies;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Shared.Dto.Articles;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;
using Backend.DataProviders;
using TokanPages;

namespace Backend.IntegrationTests.Controllers
{
    public class ArticlesControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;
        
        public ArticlesControllerTest(CustomWebApplicationFactory<Startup> AWebAppFactory)
            => FWebAppFactory = AWebAppFactory;
        
        [Fact]
        public async Task GivenAllFieldsAreCorrect_WhenAddArticle_ShouldReturnNewGuid()
        {
            // Arrange
            const string REQUEST = "/api/v1/articles/addarticle/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new AddArticleDto
            {
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                TextToUpload = StringProvider.GetRandomString(150),
                ImageToUpload = StringProvider.GetRandomString(255).ToBase64Encode()
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.IsGuid().Should().BeTrue();
        }
        
        [Fact]
        public async Task WhenGetAllArticles_ShouldReturnCollection()
        {
            // Arrange
            const string REQUEST = "/api/v1/articles/getallarticles/?AIsPublished=false";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(REQUEST);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert
                .DeserializeObject<IEnumerable<GetAllArticlesQueryResult>>(LContent)
                .ToList();
            
            LDeserialized.Should().NotBeNullOrEmpty();
            LDeserialized.Should().HaveCountGreaterThan(0);
        }
        
        [Fact]
        public async Task GivenCorrectId_WhenGetArticle_ShouldReturnEntityAsJsonObject()
        {
            // Arrange
            var LTestUserId = Article1.FId;
            var LRequest = $"/api/v1/articles/getarticle/{LTestUserId}/";
            var LHttpClient = FWebAppFactory.CreateClient();

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<GetAllArticlesQueryResult>(LContent);
            LDeserialized.Should().NotBeNull();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenGetArticle_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            var LHttpClient = FWebAppFactory.CreateClient();
            var LRequest = $"/api/v1/articles/getarticle/4b70b8e4-8a9a-4bdd-b649-19c128743b0d/";

            // Act
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
        }
        
        [Fact]
        public async Task GivenIncorrectId_WhenRemoveSubscriber_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            const string REQUEST = "/api/v1/articles/removearticle/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new RemoveArticleDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2")
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
        }
        
        [Fact]
        public async Task GivenIncorrectId_WhenUpdateArticle_ShouldReturnJsonObjectWithError()
        {
            // Arrange
            const string REQUEST = "/api/v1/articles/updatearticle/";
            var LNewRequest = new HttpRequestMessage(HttpMethod.Post, REQUEST);

            var LPayLoad = new UpdateArticleDto
            {
                Id = Guid.Parse("5a4b2494-e04b-4297-9dd8-3327837ea4e2"),
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                TextToUpload = StringProvider.GetRandomString(150),
                ImageToUpload = StringProvider.GetRandomString(255).ToBase64Encode(),
                IsPublished = false,
                AddToLikes = 0,
                UpReadCount = false
            };

            var LHttpClient = FWebAppFactory.CreateClient();
            LNewRequest.Content = new StringContent(JsonConvert.SerializeObject(LPayLoad), System.Text.Encoding.Default, "application/json");

            // Act
            var LResponse = await LHttpClient.SendAsync(LNewRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            LContent.Should().Contain(ErrorCodes.ARTICLE_DOES_NOT_EXISTS);
        }
    }
}