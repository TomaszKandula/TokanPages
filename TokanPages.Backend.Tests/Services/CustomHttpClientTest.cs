namespace TokanPages.Backend.Tests.Services
{
    using Xunit;
    using Moq;
    using Moq.Protected;
    using FluentAssertions;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Collections;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Core.Extensions;
    using Core.Utilities.CustomHttpClient;
    using Core.Utilities.CustomHttpClient.Models;

    public class CustomHttpClientTest : TestBase
    {
        public enum TestCasesEnums
        {
            EmptyKey1,
            EmptyKey2
        }

        [Fact]
        public async Task GivenValidConfigurationWithoutPayload_WhenInvokeExecute_ShouldSucceed()
        {
            // Arrange
            var configuration = new Configuration
            {
                Url = "http://localhost:5000/",
                Method = "GET"
            };

            var httpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
            
            // Act
            var customHttpClient = new CustomHttpClient(httpClient);
            var result = await customHttpClient.Execute(configuration, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.ContentType?.MediaType.Should().Be("text/plain");
            result.ContentType?.CharSet.Should().Be("utf-8");
            result.Content.Should().BeEmpty();
        }

        [Fact]
        public async Task GivenValidConfigurationWithPayload_WhenInvokeExecute_ShouldSucceed()
        {
            // Arrange
            var configuration = new Configuration
            {
                Url = "http://localhost:5000/",
                Method = "POST"
            };

            var stringContent = DataUtilityService.GetRandomString();
            var httpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(stringContent));
            
            // Act
            var customHttpClient = new CustomHttpClient(httpClient);
            var result = await customHttpClient.Execute(configuration, CancellationToken.None);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.ContentType?.MediaType.Should().Be("text/plain");
            result.ContentType?.CharSet.Should().Be("utf-8");
            result.Content.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GivenMissingUrlInConfiguration_WhenInvokeExecute_ShouldThrowError()
        {
            // Arrange
            var configuration = new Configuration
            {
                Url = string.Empty,
                Method = "GET"
            };

            var httpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
            
            // Act
            var customHttpClient = new CustomHttpClient(httpClient);
            var result = await Assert.ThrowsAsync<ArgumentException>(() => customHttpClient.Execute(configuration, CancellationToken.None));

            // Assert
            result.Message.Should().Be("Argument 'Url' cannot be null or empty.");
        }

        [Fact]
        public async Task GivenMissingMethodInConfiguration_WhenInvokeExecute_ShouldThrowError()
        {
            // Arrange
            var configuration = new Configuration
            {
                Url = "http://localhost:5000/",
                Method = string.Empty
            };

            var httpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
            
            // Act
            var customHttpClient = new CustomHttpClient(httpClient);
            var result = await Assert.ThrowsAsync<ArgumentException>(() => customHttpClient.Execute(configuration, CancellationToken.None));

            // Assert
            result.Message.Should().Be("Argument 'Method' cannot be null or empty.");
        }

        [Fact]
        public void GivenLoginAndPassword_WhenSetAuthentication_ShouldReturnBase64String()
        {
            // Arrange
            var httpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
            
            // Act
            var customHttpClient = new CustomHttpClient(httpClient);
            var result = customHttpClient.SetAuthentication(DataUtilityService.GetRandomString(), DataUtilityService.GetRandomString());

            // Assert
            result.Should().NotBeNullOrEmpty();
            var strings = result.Split(" ");
            strings[0].Should().Be("Basic");
            strings[1].IsBase64String().Should().BeTrue();
        }

        [Fact]
        public void GivenMissingLoginAndPresentPassword_WhenSetAuthentication_ShouldThrowError()
        {
            // Arrange
            var httpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
            var customHttpClient = new CustomHttpClient(httpClient);
            
            // Act
            // Assert
            var result = Assert.Throws<ArgumentException>(() => customHttpClient.SetAuthentication(string.Empty, DataUtilityService.GetRandomString()));
            result.Message.Should().Be("Argument 'login' cannot be null or empty.");
        }

        [Fact]
        public void GivenToken_WhenInvokeSetAuthentication_ShouldReturnPlainString()
        {
            // Arrange
            var httpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
            var customHttpClient = new CustomHttpClient(httpClient);
            
            // Act
            var result = customHttpClient.SetAuthentication(DataUtilityService.GetRandomString());
            
            // Assert
            var strings = result.Split(" ");
            strings[0].Should().Be("Bearer");
            strings[1].Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GivenMissingToken_WhenInvokeSetAuthentication_ShouldThrowError()
        {
            // Arrange
            var httpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
            var customHttpClient = new CustomHttpClient(httpClient);
            
            // Act
            // Assert
            var result = Assert.Throws<ArgumentException>(() => customHttpClient.SetAuthentication(string.Empty));
            result.Message.Should().Be("Argument 'token' cannot be null or empty.");
        }

        [Fact]
        public void GivenNonEmptyParameterList_WhenGetFirstEmptyParameterName_ShouldReturnEmptyString()
        {
            // Arrange
            var testList = new Dictionary<string, string>
            {
                ["Key1"] = "Value1",
                ["Key2"] = "Value2"
            };

            var httpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));

            // Act
            var customHttpClient = new CustomHttpClient(httpClient);
            var result = customHttpClient.GetFirstEmptyParameterName(testList);

            // Assert
            result.Should().BeEmpty();
        }

        [Theory]
        [ClassData(typeof(TestCases))]
        public void GivenListWithEmptyParameter_WhenGetFirstEmptyParameterName_ShouldReturnEmptyString(Dictionary<string, string> items, TestCasesEnums cases)
        {
            // Arrange
            var httpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));

            // Act
            var customHttpClient = new CustomHttpClient(httpClient);
            var result = customHttpClient.GetFirstEmptyParameterName(items);

            // Assert
            switch (cases)
            {
                case TestCasesEnums.EmptyKey1:
                    result.Should().Be("Key1");
                    break;
                case TestCasesEnums.EmptyKey2:
                    result.Should().Be("Key2");
                    break;
            }
        }

        private class TestCases : IEnumerable<object[]>
        {
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
            
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { new Dictionary<string, string> { ["Key1"] = "", ["Key2"] = "Value2" }, TestCasesEnums.EmptyKey1 };
                yield return new object[] { new Dictionary<string, string> { ["Key1"] = "Value1", ["Key2"] = "" }, TestCasesEnums.EmptyKey2 };
            }
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
}