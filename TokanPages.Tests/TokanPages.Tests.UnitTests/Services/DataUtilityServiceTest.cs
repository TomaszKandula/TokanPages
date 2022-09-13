using System;
using FluentAssertions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Services;

public class DataUtilityServiceTest : TestBase
{
    [Fact]
    public void GivenZeroLength_WhenInvokeGetRandomString_ShouldReturnEmptyString()
    {
        // Arrange 
        // Act
        var result = DataUtilityService.GetRandomString(0);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void GivenNoParameters_WhenInvokeGetRandomString_ShouldReturnTwelveChars()
    {
        // Arrange 
        // Act
        var result = DataUtilityService.GetRandomString();

        // Assert
        result.Length.Should().Be(12);
    }
        
    [Fact]
    public void GivenPrefixAndLength_WhenInvokeGetRandomString_ShouldReturnTwelveCharsWithPrefix()
    {
        // Arrange 
        const string prefix = "TEST_";
        const int length = 12;
        var prefixLength = prefix.Length;
            
        // Act
        var result = DataUtilityService.GetRandomString(length, prefix);

        // Assert
        result.Length.Should().Be(length + prefixLength);
        result.Should().Contain(prefix);
    }

    [Fact]
    public void GivenNoParameters_WhenInvokeGetRandomEmail_ShouldReturnGmailEmailAddress()
    {
        // Arrange
        // Act
        var result = DataUtilityService.GetRandomEmail();

        // Assert
        result.Length.Should().BeGreaterThan(12);
        result.Should().Contain("@gmail.com");
    }

    [Fact]
    public void GivenLengthAndDomain_WhenInvokeGetRandomEmail_ShouldReturnNewEmailAddress()
    {
        // Arrange
        const string domain = "hotmail.com";
        const int length = 20;
        var expectedLength = domain.Length + length + 1;
            
        // Act
        var result = DataUtilityService.GetRandomEmail(length, domain);

        // Assert
        result.Length.Should().Be(expectedLength);
        result.Should().Contain(domain);
    }

    [Fact]
    public void GivenNoParameters_WhenInvokeGetRandom_ShouldReturnNewByteArray()
    {
        // Arrange
        const int expectedLength = 12 * 1024;
            
        // Act
        var result = DataUtilityService.GetRandomStream();

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(expectedLength);
    }

    [Fact]
    public void GivenSizeParameter_WhenInvokeGetRandom_ShouldReturnNewByteArray()
    {
        // Arrange
        const int sizeInKb = 150;
        const int expectedLength = sizeInKb * 1024;
            
        // Act
        var result = DataUtilityService.GetRandomStream(sizeInKb);

        // Assert
        result.Should().NotBeNull();
        result.Length.Should().Be(expectedLength);
    }

    [Fact]
    public void GivenNoParameters_WhenInvokeGetRandomInteger_ShouldReturnInteger()
    {
        // Arrange
        // Act
        var result = DataUtilityService.GetRandomInteger();

        // Assert
        result.Should().BeInRange(0, 12);
    }

    [Fact]
    public void GivenNoParameters_WhenInvokeGetRandomDecimal_ShouldReturnDecimal()
    {
        // Arrange
        // Act
        var result = DataUtilityService.GetRandomDecimal();

        // Assert
        result.Should().BeInRange(0m, 9999m);
    }
        
    [Theory]
    [InlineData(-3, 3)]
    [InlineData(12, 50)]
    [InlineData(100, 200)]
    public void GivenMinAndMaxParameters_WhenInvokeGetRandomInteger_ShouldReturnInteger(int min, int max)
    {
        // Arrange
        // Act
        var result = DataUtilityService.GetRandomInteger(min, max);

        // Assert
        result.Should().BeInRange(min, max);
    }

    [Theory]
    [InlineData(-3.3, 5.6)]
    [InlineData(10.1, 20.91)]
    [InlineData(100.5, 205.45)]
    public void GivenMinAndMaxParameters_WhenInvokeGetRandomDecimal_ShouldReturnDecimal(int min, int max)
    {
        // Arrange
        // Act
        var result = DataUtilityService.GetRandomDecimal(min, max);
            
        // Assert
        result.Should().BeInRange(min, max);
    }

    [Fact]
    public void GivenType_WhenInvokeGetRandom_ShouldReturnEnumOfGivenType()
    {
        // Arrange
        // Act
        var result = DataUtilityService.GetRandomEnum<DayOfWeek>();

        // Assert
        result.Should().NotBe(null);
        result.GetType().Should().Be<DayOfWeek>();
    }

    [Fact]
    public void GivenNoParameters_WhenInvokeGetRandomDateTime_ShouldReturnDateTime()
    {
        // Arrange
        // Act
        var result = DataUtilityService.GetRandomDateTime();

        // Assert
        result.Should().HaveYear(2020);
        result.Month.Should().BeInRange(1, 12);
        result.Day.Should().BeInRange(1, 31);
    }
        
    [Fact]
    public void GivenMinAndMaxDateTime_WhenInvokeGetRandomDateTime_ShouldReturnDateTime()
    {
        // Arrange
        var dateTimeMin = new DateTime(2020, 1, 1);
        var dateTimeMax = new DateTime(2021, 10, 15);
            
        // Act
        var result = DataUtilityService.GetRandomDateTime(dateTimeMin, dateTimeMax);

        // Assert
        result.Year.Should().BeInRange(2020, 2021);
        result.Month.Should().BeInRange(1, 12);
        result.Day.Should().BeInRange(1, 31);
    }

    [Fact]
    public void GivenOnlyDefaultYear_WhenInvokeGetRandomDateTime_ShouldReturnDateTime()
    {
        // Arrange
        // Act
        var result = DataUtilityService.GetRandomDateTime(null, null, 2015);

        // Assert
        result.Year.Should().BeInRange(2015, 2015);
        result.Month.Should().BeInRange(1, 12);
        result.Day.Should().BeInRange(1, 31);
    }

    [Fact]
    public void GivenFlagFalse_WhenGetRandomIpAddress_ShouldReturnRandomIPv4()
    {
        // Arrange
        // Act
        var result = DataUtilityService.GetRandomIpAddress();
            
        // Assert
        result.AddressFamily.Should().Be(System.Net.Sockets.AddressFamily.InterNetwork);
    }

    [Fact]
    public void GivenFlagTrue_WhenGetRandomIpAddress_ShouldReturnRandomIPv6()
    {
        // Arrange
        // Act
        var result = DataUtilityService.GetRandomIpAddress(true);
            
        // Assert
        result.AddressFamily.Should().Be(System.Net.Sockets.AddressFamily.InterNetworkV6);
    }
}