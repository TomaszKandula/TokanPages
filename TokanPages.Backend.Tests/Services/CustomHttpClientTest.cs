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
            var LConfiguration = new Configuration
            {
                Url = "http://localhost:5000/",
                Method = "GET"
            };

            var LHttpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
            
            // Act
            var LCustomHttpClient = new CustomHttpClient(LHttpClient);
            var LResult = await LCustomHttpClient.Execute(LConfiguration, CancellationToken.None);

            // Assert
            LResult.StatusCode.Should().Be(HttpStatusCode.OK);
            LResult.ContentType?.MediaType.Should().Be("text/plain");
            LResult.ContentType?.CharSet.Should().Be("utf-8");
            LResult.Content.Should().BeEmpty();
        }

        [Fact]
        public async Task GivenValidConfigurationWithPayload_WhenInvokeExecute_ShouldSucceed()
        {
            // Arrange
            var LConfiguration = new Configuration
            {
                Url = "http://localhost:5000/",
                Method = "POST"
            };

            var LStringContent = DataUtilityService.GetRandomString();
            var LHttpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(LStringContent));
            
            // Act
            var LCustomHttpClient = new CustomHttpClient(LHttpClient);
            var LResult = await LCustomHttpClient.Execute(LConfiguration, CancellationToken.None);

            // Assert
            LResult.StatusCode.Should().Be(HttpStatusCode.OK);
            LResult.ContentType?.MediaType.Should().Be("text/plain");
            LResult.ContentType?.CharSet.Should().Be("utf-8");
            LResult.Content.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GivenMissingUrlInConfiguration_WhenInvokeExecute_ShouldThrowError()
        {
            // Arrange
            var LConfiguration = new Configuration
            {
                Url = string.Empty,
                Method = "GET"
            };

            var LHttpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
            
            // Act
            var LCustomHttpClient = new CustomHttpClient(LHttpClient);
            var LResult = await Assert.ThrowsAsync<ArgumentException>(() => LCustomHttpClient.Execute(LConfiguration, CancellationToken.None));

            // Assert
            LResult.Message.Should().Be("Argument 'Url' cannot be null or empty.");
        }

        [Fact]
        public async Task GivenMissingMethodInConfiguration_WhenInvokeExecute_ShouldThrowError()
        {
            // Arrange
            var LConfiguration = new Configuration
            {
                Url = "http://localhost:5000/",
                Method = string.Empty
            };

            var LHttpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
            
            // Act
            var LCustomHttpClient = new CustomHttpClient(LHttpClient);
            var LResult = await Assert.ThrowsAsync<ArgumentException>(() => LCustomHttpClient.Execute(LConfiguration, CancellationToken.None));

            // Assert
            LResult.Message.Should().Be("Argument 'Method' cannot be null or empty.");
        }

        [Fact]
        public void GivenLoginAndPassword_WhenSetAuthentication_ShouldReturnBase64String()
        {
            // Arrange
            var LHttpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
            
            // Act
            var LCustomHttpClient = new CustomHttpClient(LHttpClient);
            var LResult = LCustomHttpClient.SetAuthentication(DataUtilityService.GetRandomString(), DataUtilityService.GetRandomString());

            // Assert
            LResult.Should().NotBeNullOrEmpty();
            var LStrings = LResult.Split(" ");
            LStrings[0].Should().Be("Basic");
            LStrings[1].IsBase64String().Should().BeTrue();
        }

        [Fact]
        public void GivenMissingLoginAndPresentPassword_WhenSetAuthentication_ShouldThrowError()
        {
            // Arrange
            var LHttpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
            var LCustomHttpClient = new CustomHttpClient(LHttpClient);
            
            // Act
            // Assert
            var LResult = Assert.Throws<ArgumentException>(() => LCustomHttpClient.SetAuthentication(string.Empty, DataUtilityService.GetRandomString()));
            LResult.Message.Should().Be("Argument 'ALogin' cannot be null or empty.");
        }

        [Fact]
        public void GivenToken_WhenInvokeSetAuthentication_ShouldReturnPlainString()
        {
            // Arrange
            var LHttpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
            var LCustomHttpClient = new CustomHttpClient(LHttpClient);
            
            // Act
            var LResult = LCustomHttpClient.SetAuthentication(DataUtilityService.GetRandomString());
            
            // Assert
            var LStrings = LResult.Split(" ");
            LStrings[0].Should().Be("Bearer");
            LStrings[1].Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void GivenMissingToken_WhenInvokeSetAuthentication_ShouldThrowError()
        {
            // Arrange
            var LHttpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));
            var LCustomHttpClient = new CustomHttpClient(LHttpClient);
            
            // Act
            // Assert
            var LResult = Assert.Throws<ArgumentException>(() => LCustomHttpClient.SetAuthentication(string.Empty));
            LResult.Message.Should().Be("Argument 'AToken' cannot be null or empty.");
        }

        [Fact]
        public void GivenNonEmptyParameterList_WhenGetFirstEmptyParameterName_ShouldReturnEmptyString()
        {
            // Arrange
            var LTestList = new Dictionary<string, string>
            {
                ["Key1"] = "Value1",
                ["Key2"] = "Value2"
            };

            var LHttpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));

            // Act
            var LCustomHttpClient = new CustomHttpClient(LHttpClient);
            var LResult = LCustomHttpClient.GetFirstEmptyParameterName(LTestList);

            // Assert
            LResult.Should().BeEmpty();
        }

        [Theory]
        [ClassData(typeof(TestCases))]
        public void GivenListWithEmptyParameter_WhenGetFirstEmptyParameterName_ShouldReturnEmptyString(Dictionary<string, string> AItems, TestCasesEnums ACase)
        {
            // Arrange
            var LHttpClient = SetHttpClient(HttpStatusCode.OK, new StringContent(string.Empty));

            // Act
            var LCustomHttpClient = new CustomHttpClient(LHttpClient);
            var LResult = LCustomHttpClient.GetFirstEmptyParameterName(AItems);

            // Assert
            switch (ACase)
            {
                case TestCasesEnums.EmptyKey1:
                    LResult.Should().Be("Key1");
                    break;
                case TestCasesEnums.EmptyKey2:
                    LResult.Should().Be("Key2");
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

        private static Mock<HttpMessageHandler> SetMockedHttpMessageHandler(HttpResponseMessage AHttpResponseMessage)
        {
            var LMockedHttpMessageHandler = new Mock<HttpMessageHandler>();
            
            LMockedHttpMessageHandler
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync", 
                    ItExpr.IsAny<HttpRequestMessage>(), 
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(AHttpResponseMessage);

            return LMockedHttpMessageHandler;
        }

        private static HttpClient SetHttpClient(HttpStatusCode AHttpStatusCode, HttpContent AHttpContent)
        {
            var LHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = AHttpStatusCode,
                Content = AHttpContent
            };
            var LMockedHttpMessageHandler = SetMockedHttpMessageHandler(LHttpResponseMessage);
            return new HttpClient(LMockedHttpMessageHandler.Object);
        }
    }
}