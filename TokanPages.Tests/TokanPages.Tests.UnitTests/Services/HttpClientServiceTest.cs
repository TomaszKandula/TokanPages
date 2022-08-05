namespace TokanPages.Tests.UnitTests.Services;

using Xunit;
using Moq;
using Moq.Protected;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Backend.Core.Exceptions;
using TokanPages.Services.HttpClientService;
using TokanPages.Services.HttpClientService.Models;

public class HttpClientServiceTest : TestBase
{
    [Fact]
    public async Task GivenValidConfigurationAndNoPayload_WhenInvokeExecute_ShouldSucceed()
    {
        // Arrange
        var configuration = new Configuration
        {
            Url = "http://localhost:5000/",
            Method = "GET"
        };

        var httpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
        var customHttpClient = new HttpClientService(httpClient);

        // Act
        var result = await customHttpClient.Execute(configuration, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.ContentType?.MediaType.Should().Be("text/plain");
        result.ContentType?.CharSet.Should().Be("utf-8");
        result.Content.Should().BeEmpty();
    }

    [Fact]
    public async Task GivenValidConfigurationAndPayload_WhenInvokeExecute_ShouldSucceed()
    {
        // Arrange
        var configuration = new Configuration
        {
            Url = "http://localhost:5000/",
            Method = "POST"
        };

        var stringContent = DataUtilityService.GetRandomString();
        var httpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(stringContent));
        var customHttpClient = new HttpClientService(httpClient);

        // Act
        var result = await customHttpClient.Execute(configuration, CancellationToken.None);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        result.ContentType?.MediaType.Should().Be("text/plain");
        result.ContentType?.CharSet.Should().Be("utf-8");
        result.Content.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GivenMissingUrl_WhenInvokeExecute_ShouldThrowError()
    {
        // Arrange
        var configuration = new Configuration
        {
            Url = string.Empty,
            Method = "GET"
        };

        var httpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
        var customHttpClient = new HttpClientService(httpClient);

        // Act
        var result = await Assert.ThrowsAsync<BusinessException>(() => customHttpClient.Execute(configuration, CancellationToken.None));

        // Assert
        result.Message.Should().Be("Argument 'Url' cannot be null or empty.");
    }

    [Fact]
    public async Task GivenMissingMethod_WhenInvokeExecute_ShouldThrowError()
    {
        // Arrange
        var configuration = new Configuration
        {
            Url = "http://localhost:5000/",
            Method = string.Empty
        };

        var httpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
        var customHttpClient = new HttpClientService(httpClient);

        // Act
        var result = await Assert.ThrowsAsync<BusinessException>(() => customHttpClient.Execute(configuration, CancellationToken.None));

        // Assert
        result.Message.Should().Be("Argument 'Method' cannot be null or empty.");
    }

    private static Mock<HttpMessageHandler> SetMockedHttpMessageHandler(HttpResponseMessage httpResponseMessage)
    {
        var mockedHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockedHttpMessageHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync", 
                ItExpr.IsAny<HttpRequestMessage>(), 
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(httpResponseMessage);

        return mockedHttpMessageHandler;
    }

    private static HttpClient SetHttpClient(HttpStatusCode httpStatusCode, HttpContent httpContent)
    {
        var httpResponseMessage = new HttpResponseMessage
        {
            StatusCode = httpStatusCode,
            Content = httpContent
        };

        var mockedHttpMessageHandler = SetMockedHttpMessageHandler(httpResponseMessage);
        return new HttpClient(mockedHttpMessageHandler.Object);
    }
}