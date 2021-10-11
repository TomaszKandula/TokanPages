namespace TokanPages.WebApi.Tests.ArticlesController
{
    using System;
    using System.Net;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Backend.Cqrs.Handlers.Queries.Articles;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public partial class ArticlesControllerTest
    {
        [Fact]
        public async Task WhenGetAllArticles_ShouldReturnCollection()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/GetAllArticles/?AIsPublished=false";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);
            await EnsureStatusCode(LResponse, HttpStatusCode.OK);

            // Assert
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = (JsonConvert
                    .DeserializeObject<IEnumerable<GetAllArticlesQueryResult>>(LContent) ?? Array.Empty<GetAllArticlesQueryResult>())
                .ToList();
            
            LDeserialized.Should().NotBeNullOrEmpty();
            LDeserialized.Should().HaveCountGreaterThan(0);
        }
    }
}