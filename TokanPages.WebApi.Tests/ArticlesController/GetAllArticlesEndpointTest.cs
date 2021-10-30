namespace TokanPages.WebApi.Tests.ArticlesController
{
    using Xunit;
    using Newtonsoft.Json;
    using FluentAssertions;
    using System;
    using System.Net;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Backend.Cqrs.Handlers.Queries.Articles;

    public partial class ArticlesControllerTest
    {
        [Fact]
        public async Task WhenGetAllArticles_ShouldReturnCollection()
        {
            // Arrange
            var request = $"{ApiBaseUrl}/GetAllArticles/?IsPublished=false";

            // Act
            var httpClient = _webApplicationFactory.CreateClient();
            var response = await httpClient.GetAsync(request);
            await EnsureStatusCode(response, HttpStatusCode.OK);

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            content.Should().NotBeNullOrEmpty();

            var deserialized = (JsonConvert
                    .DeserializeObject<IEnumerable<GetAllArticlesQueryResult>>(content) ?? Array.Empty<GetAllArticlesQueryResult>())
                .ToList();
            
            deserialized.Should().NotBeNullOrEmpty();
            deserialized.Should().HaveCountGreaterThan(0);
        }
    }
}