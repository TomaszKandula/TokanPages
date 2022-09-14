using FluentAssertions;
using Newtonsoft.Json.Linq;
using TokanPages.Backend.Core.Utilities.JsonSerializer;
using Xunit;

namespace TokanPages.Tests.UnitTests.Services;

public class JsonSerializerTest : TestBase
{
    private static string ValidJsonList => "[{\"Key1\":\"Value1\",\"Key2\":\"Value2\"},{\"Key1\":\"Value1\",\"Key2\":\"Value2\"}]";

    private static string ValidJsonString => "{\"Key1\":\"Value1\",\"Key2\":\"Value2\"}";

    private static string InvalidJsonString => "{\"Key1\":\"Value1\"\"Key2\":\"Value2\"";

    private class TestClass
    {
        public string? Key1 { get; init; }

        public string? Key2 { get; init; }
    }
        
    [Fact]
    public void GivenValidObject_WhenInvokeSerialize_ShouldSucceed()
    {
        // Arrange
        var testObject = new TestClass
        {
            Key1 = "Value1",
            Key2 = "Value2"
        };
            
        var jsonSerializer = new JsonSerializer();

        // Act
        var result = jsonSerializer.Serialize(testObject);

        // Assert
        result.Should().Be(ValidJsonString);
    }

    [Fact]
    public void GivenValidString_WhenInvokeDeserialize_ShouldSucceed()
    {
        // Arrange
        var jsonSerializer = new JsonSerializer();

        // Act
        var result = jsonSerializer.Deserialize<TestClass>(ValidJsonString);

        // Assert
        result.Should().BeOfType<TestClass>();
        result.Key1.Should().Be("Value1");
        result.Key2.Should().Be("Value2");
    }

    [Fact]
    public void GivenEmptyString_WhenInvokeDeserialize_ShouldThrowError()
    {
        // Arrange
        var jsonSerializer = new JsonSerializer();

        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => jsonSerializer.Deserialize<TestClass>(string.Empty));
    }

    [Fact]
    public void GivenInvalidString_WhenInvokeDeserialize_ShouldThrowError()
    {
        // Arrange
        var jsonSerializer = new JsonSerializer();

        // Act
        // Assert
        Assert.Throws<Newtonsoft.Json.JsonReaderException>(() => jsonSerializer.Deserialize<TestClass>(InvalidJsonString));
    }

    [Fact]
    public void GivenValidJsonString_WhenParse_ShouldReturnJObject()
    {
        // Arrange
        var jsonSerializer = new JsonSerializer();

        // Act
        var parsed = jsonSerializer.Parse(ValidJsonString);

        // Assert
        parsed.Should().BeOfType<JObject>();
    }

    [Fact]
    public void GivenInvalidJsonString_WhenParse_ShouldThrowError()
    {
        // Arrange
        var jsonSerializer = new JsonSerializer();

        // Act
        // Assert
        Assert.Throws<Newtonsoft.Json.JsonReaderException>(() => jsonSerializer.Parse(InvalidJsonString));
    }

    [Fact]
    public void GivenEmptyJsonString_WhenParse_ShouldReturnNull()
    {
        // Arrange
        var jsonSerializer = new JsonSerializer();

        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => jsonSerializer.Parse(string.Empty));
    }

    [Fact]
    public void GivenParsedJsonList_WhenInvokeMapObjects_ShouldReturnListOfObjects()
    {
        // Arrange
        var jsonSerializer = new JsonSerializer();
        var parsed = jsonSerializer.Parse(ValidJsonList);

        // Act
        var result = jsonSerializer.MapObjects<TestClass>(parsed);

        // Assert
        var testClasses = result.ToList();
        testClasses.Should().HaveCount(2);
    }

    [Fact]
    public void GivenParsedJsonObject_WhenInvokeMapObjects_ShouldReturnEmptyList()
    {
        // Arrange
        var jsonSerializer = new JsonSerializer();
        var parsed = jsonSerializer.Parse(ValidJsonString);

        // Act
        var result = jsonSerializer.MapObjects<TestClass>(parsed);

        // Assert
        var testClasses = result.ToList();
        testClasses.Should().HaveCount(0);
    }

    [Fact]
    public void GivenParsedJsonObject_WhenInvokeMapObject_ShouldSucceed()
    {
        // Arrange
        var jsonSerializer = new JsonSerializer();
        var parsed = jsonSerializer.Parse(ValidJsonString);

        // Act
        var result = jsonSerializer.MapObject<TestClass>(parsed);

        // Assert
        result.Key1.Should().Be("Value1");
        result.Key2.Should().Be("Value2");
    }

    [Fact]
    public void GivenParsedJsonList_WhenInvokeMapObject_ShouldReturnEmptyObject()
    {
        // Arrange
        var jsonSerializer = new JsonSerializer();
        var parsed = jsonSerializer.Parse(ValidJsonList);

        // Act
        var result = jsonSerializer.MapObject<TestClass>(parsed);

        // Assert
        result.Key1.Should().BeNull();
        result.Key2.Should().BeNull();
    }
}