namespace TokanPages.WebApi.Tests.Controllers
{
    using System.Net;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.Extensions.Configuration;
    using Backend.Shared.Models;
    using FluentAssertions;
    using Newtonsoft.Json;
    using Xunit;

    public class HealthControllerTest : TestBase, IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private const string API_BASE_URL = "/api/v1/health";

        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;

        public HealthControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory) => FWebAppFactory = AWebAppFactory;

        [Fact]
        public async Task GivenCorrectConfiguration_WhenRequestStatusCheck_ShouldReturnSuccessful()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/Status/";
        
            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);
        
            // Assert
            LResponse.EnsureSuccessStatusCode();
        
            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            
            var LDeserialized = JsonConvert.DeserializeObject<ActionResultModel>(LContent);
            LDeserialized.Should().NotBeNull();
            LDeserialized.IsSucceeded.Should().BeTrue();
            LDeserialized.ErrorCode.Should().BeNull();
            LDeserialized.ErrorDesc.Should().BeNull();
        }
        
        [Fact]
        public async Task GivenInvalidSmtpServer_WhenRequestStatusCheck_ShouldThrowError()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/Status/";
            var LWebAppFactory = FWebAppFactory.WithWebHostBuilder(ABuilder =>
            {
                ABuilder.ConfigureAppConfiguration((AContext, AConfigBuilder) =>
                {
                    AConfigBuilder.AddInMemoryCollection(
                        new Dictionary<string, string> { ["SmtpServer:Server"] = DataProviderService.GetRandomString() });
                });
            });
            
            // Act
            var LHttpClient = LWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            
            var LDeserialized = JsonConvert.DeserializeObject<ActionResultModel>(LContent);
            LDeserialized.Should().NotBeNull();
            LDeserialized.IsSucceeded.Should().BeFalse();
            LDeserialized.ErrorCode.Should().NotBeEmpty();
            LDeserialized.ErrorDesc.Should().NotBeEmpty();
        }
        
        [Fact]
        public async Task GivenInvalidDatabaseServer_WhenRequestStatusCheck_ShouldThrowError()
        {
            // Arrange
            var LRequest = $"{API_BASE_URL}/Status/";
            var LWebAppFactory = FWebAppFactory.WithWebHostBuilder(ABuilder =>
            {
                ABuilder.ConfigureAppConfiguration((AContext, AConfigBuilder) =>
                {
                    AConfigBuilder.AddInMemoryCollection(
                        new Dictionary<string, string> { ["ConnectionStrings:DbConnectTest"] = DataProviderService.GetRandomString() });
                });
            });
            
            // Act
            var LHttpClient = LWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(LRequest);

            // Assert
            LResponse.StatusCode.Should().Be(HttpStatusCode.InternalServerError);

            var LContent = await LResponse.Content.ReadAsStringAsync();
            LContent.Should().NotBeNullOrEmpty();
            
            var LDeserialized = JsonConvert.DeserializeObject<ActionResultModel>(LContent);
            LDeserialized.Should().NotBeNull();
            LDeserialized.IsSucceeded.Should().BeFalse();
            LDeserialized.ErrorCode.Should().NotBeEmpty();
            LDeserialized.ErrorDesc.Should().NotBeEmpty();
        }
    }
}