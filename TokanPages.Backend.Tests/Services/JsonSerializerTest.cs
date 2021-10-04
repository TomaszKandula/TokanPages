namespace TokanPages.Backend.Tests.Services
{
    using Xunit;
    using System.Linq;
    using FluentAssertions;
    using Newtonsoft.Json.Linq;
    using Core.Utilities.JsonSerializer;

    public class JsonSerializerTest : TestBase
    {
        private static string ValidJsonList => "[{\"Key1\":\"Value1\",\"Key2\":\"Value2\"},{\"Key1\":\"Value1\",\"Key2\":\"Value2\"}]";
        
        private static string ValidJsonString => "{\"Key1\":\"Value1\",\"Key2\":\"Value2\"}";

        private static string InvalidJsonString => "{\"Key1\":\"Value1\"\"Key2\":\"Value2\"";

        private class TestClass
        {
            public string Key1 { get; init; }

            public string Key2 { get; init; }
        }
        
        [Fact]
        public void GivenValidObject_WhenInvokeSerialize_ShouldSucceed()
        {
            // Arrange
            var LTestObject = new TestClass
            {
                Key1 = "Value1",
                Key2 = "Value2"
            };
            
            // Act
            var LJsonSerializer = new JsonSerializer();
            var LResult = LJsonSerializer.Serialize(LTestObject);

            // Assert
            LResult.Should().Be(ValidJsonString);
        }

        [Fact]
        public void GivenValidString_WhenInvokeDeserialize_ShouldSucceed()
        {
            // Arrange
            // Act
            var LJsonSerializer = new JsonSerializer();
            var LResult = LJsonSerializer.Deserialize<TestClass>(ValidJsonString);

            // Assert
            LResult.Should().BeOfType<TestClass>();
            LResult.Key1.Should().Be("Value1");
            LResult.Key2.Should().Be("Value2");
        }

        [Fact]
        public void GivenInvalidString_WhenInvokeDeserialize_ShouldThrowError()
        {
            // Arrange
            // Act
            // Assert
            var LJsonSerializer = new JsonSerializer();
            Assert.Throws<Newtonsoft.Json.JsonReaderException>(() => LJsonSerializer.Deserialize<TestClass>(InvalidJsonString));
        }

        [Fact]
        public void GivenValidJsonString_WhenParse_ShouldReturnJObject()
        {
            // Arrange
            // Act
            var LJsonSerializer = new JsonSerializer();
            var LParsed = LJsonSerializer.Parse(ValidJsonString);

            // Assert
            LParsed.Should().BeOfType<JObject>();
        }

        [Fact]
        public void GivenInvalidJsonString_WhenParse_ShouldThrowError()
        {
            // Arrange
            // Act
            // Assert
            var LJsonSerializer = new JsonSerializer();
            Assert.Throws<Newtonsoft.Json.JsonReaderException>(() => LJsonSerializer.Parse(InvalidJsonString));
        }

        [Fact]
        public void GivenEmptyJsonString_WhenParse_ShouldReturnNull()
        {
            // Arrange
            // Act
            var LJsonSerializer = new JsonSerializer();
            var LParsed = LJsonSerializer.Parse(string.Empty);

            // Assert
            LParsed.Should().BeNull();
        }

        [Fact]
        public void GivenParsedJsonList_WhenInvokeMapObjects_ShouldReturnListOfObjects()
        {
            // Arrange
            var LJsonSerializer = new JsonSerializer();
            var LParsed = LJsonSerializer.Parse(ValidJsonList);

            // Act
            var LResult = LJsonSerializer.MapObjects<TestClass>(LParsed);

            // Assert
            var LTestClasses = LResult.ToList();
            LTestClasses.Should().HaveCount(2);
        }

        [Fact]
        public void GivenParsedJsonObject_WhenInvokeMapObjects_ShouldReturnEmptyList()
        {
            // Arrange
            var LJsonSerializer = new JsonSerializer();
            var LParsed = LJsonSerializer.Parse(ValidJsonString);

            // Act
            var LResult = LJsonSerializer.MapObjects<TestClass>(LParsed);

            // Assert
            var LTestClasses = LResult.ToList();
            LTestClasses.Should().HaveCount(0);
        }

        [Fact]
        public void GivenParsedJsonObject_WhenInvokeMapObject_ShouldSucceed()
        {
            // Arrange
            var LJsonSerializer = new JsonSerializer();
            var LParsed = LJsonSerializer.Parse(ValidJsonString);

            // Act
            var LResult = LJsonSerializer.MapObject<TestClass>(LParsed);

            // Assert
            LResult.Key1.Should().Be("Value1");
            LResult.Key2.Should().Be("Value2");
        }

        [Fact]
        public void GivenParsedJsonList_WhenInvokeMapObject_ShouldReturnEmptyObject()
        {
            // Arrange
            var LJsonSerializer = new JsonSerializer();
            var LParsed = LJsonSerializer.Parse(ValidJsonList);

            // Act
            var LResult = LJsonSerializer.MapObject<TestClass>(LParsed);

            // Assert
            LResult.Key1.Should().BeNull();
            LResult.Key2.Should().BeNull();
        }
    }
}