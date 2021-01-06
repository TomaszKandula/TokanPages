﻿using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using TokanPages;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Backend.IntegrationTests.Handlers.Articles
{
    
    public class GetAllArticlesQueryHandlerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {

        private readonly CustomWebApplicationFactory<Startup> FWebAppFactory;

        public GetAllArticlesQueryHandlerTest(CustomWebApplicationFactory<Startup> AWebAppFactory)
        {
            FWebAppFactory = AWebAppFactory;
        }

        [Fact]
        public async Task GetAllArticles_ShouldReturnCollection()
        {

            // Arrange
            var LRequest = $"/api/v1/articles/getallarticles/";

            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.EnsureSuccessStatusCode();

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();

            var LDeserialized = JsonConvert.DeserializeObject<IEnumerable<TokanPages.Backend.Domain.Entities.Articles>>(LContent);
            LDeserialized.Should().NotBeNullOrEmpty();
            LDeserialized.Should().HaveCountGreaterThan(0);

        }

    }

}
