using Xunit;
using Newtonsoft.Json;
using FluentAssertions;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using TokanPages.Backend.Shared.Models;
using TokanPages.Backend.Core.Generators;

namespace TokanPages.WebApi.Tests.Controllers
{
    public class HealthControllerTest : IClassFixture<CustomWebApplicationFactory<TestStartup>>
    {
        private readonly CustomWebApplicationFactory<TestStartup> FWebAppFactory;
        
        public HealthControllerTest(CustomWebApplicationFactory<TestStartup> AWebAppFactory)
            => FWebAppFactory = AWebAppFactory;

        [Fact]
        public async Task GivenCorrectConfiguration_WhenRequestStatusCheck_ShouldReturnSuccessful()
        {
            // Arrange
            const string REQUEST = "/api/v1/health/status/";
        
            // Act
            var LHttpClient = FWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(REQUEST);
        
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
            const string REQUEST = "/api/v1/health/status/";
            var LWebAppFactory = FWebAppFactory.WithWebHostBuilder(ABuilder =>
            {
                ABuilder.ConfigureAppConfiguration((AContext, AConfigBuilder) =>
                {
                    AConfigBuilder.AddInMemoryCollection(
                        new Dictionary<string, string> { ["SmtpServer:Server"] = StringProvider.GetRandomString() });
                });
            });
            
            // Act
            var LHttpClient = LWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(REQUEST);

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
            const string REQUEST = "/api/v1/health/status/";
            var LWebAppFactory = FWebAppFactory.WithWebHostBuilder(ABuilder =>
            {
                ABuilder.ConfigureAppConfiguration((AContext, AConfigBuilder) =>
                {
                    AConfigBuilder.AddInMemoryCollection(
                        new Dictionary<string, string> { ["ConnectionStrings:DbConnectTest"] = StringProvider.GetRandomString() });
                });
            });
            
            // Act
            var LHttpClient = LWebAppFactory.CreateClient();
            var LResponse = await LHttpClient.GetAsync(REQUEST);

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