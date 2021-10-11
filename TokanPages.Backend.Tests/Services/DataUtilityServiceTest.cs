namespace TokanPages.Backend.Tests.Services
{
    using System;
    using FluentAssertions;
    using Xunit;

    public class DataUtilityServiceTest : TestBase
    {
        [Fact]
        public void GivenZeroLength_WhenInvokeGetRandomString_ShouldReturnEmptyString()
        {
            // Arrange 
            // Act
            var LResult = DataUtilityService.GetRandomString(0);

            // Assert
            LResult.Should().BeEmpty();
        }

        [Fact]
        public void GivenNoParameters_WhenInvokeGetRandomString_ShouldReturnTwelveChars()
        {
            // Arrange 
            // Act
            var LResult = DataUtilityService.GetRandomString();

            // Assert
            LResult.Length.Should().Be(12);
        }
        
        [Fact]
        public void GivenPrefixAndLength_WhenInvokeGetRandomString_ShouldReturnTwelveCharsWithPrefix()
        {
            // Arrange 
            const string PREFIX = "TEST_";
            const int LENGTH = 12;
            var LPrefixLength = PREFIX.Length;
            
            // Act
            var LResult = DataUtilityService.GetRandomString(LENGTH, PREFIX);

            // Assert
            LResult.Length.Should().Be(LENGTH + LPrefixLength);
            LResult.Should().Contain(PREFIX);
        }

        [Fact]
        public void GivenNoParameters_WhenInvokeGetRandomEmail_ShouldReturnGmailEmailAddress()
        {
            // Arrange
            // Act
            var LResult = DataUtilityService.GetRandomEmail();

            // Assert
            LResult.Length.Should().BeGreaterThan(12);
            LResult.Should().Contain("@gmail.com");
        }

        [Fact]
        public void GivenLengthAndDomain_WhenInvokeGetRandomEmail_ShouldReturnNewEmailAddress()
        {
            // Arrange
            const string DOMAIN = "hotmail.com";
            const int LENGTH = 20;
            var LExpectedLength = DOMAIN.Length + LENGTH + 1;
            
            // Act
            var LResult = DataUtilityService.GetRandomEmail(LENGTH, DOMAIN);

            // Assert
            LResult.Length.Should().Be(LExpectedLength);
            LResult.Should().Contain(DOMAIN);
        }

        [Fact]
        public void GivenNoParameters_WhenInvokeGetRandom_ShouldReturnNewByteArray()
        {
            // Arrange
            const int EXPECTED_LENGTH = 12 * 1024;
            
            // Act
            var LResult = DataUtilityService.GetRandomStream();

            // Assert
            LResult.Should().NotBeNull();
            LResult.Length.Should().Be(EXPECTED_LENGTH);
        }

        [Fact]
        public void GivenSizeParameter_WhenInvokeGetRandom_ShouldReturnNewByteArray()
        {
            // Arrange
            const int SIZE_IN_KB = 150;
            const int EXPECTED_LENGTH = SIZE_IN_KB * 1024;
            
            // Act
            var LResult = DataUtilityService.GetRandomStream(SIZE_IN_KB);

            // Assert
            LResult.Should().NotBeNull();
            LResult.Length.Should().Be(EXPECTED_LENGTH);
        }

        [Fact]
        public void GivenNoParameters_WhenInvokeGetRandomInteger_ShouldReturnInteger()
        {
            // Arrange
            // Act
            var LResult = DataUtilityService.GetRandomInteger();

            // Assert
            LResult.Should().BeInRange(0, 12);
        }

        [Fact]
        public void GivenNoParameters_WhenInvokeGetRandomDecimal_ShouldReturnDecimal()
        {
            // Arrange
            // Act
            var LResult = DataUtilityService.GetRandomDecimal();

            // Assert
            LResult.Should().BeInRange(0m, 9999m);
        }
        
        [Theory]
        [InlineData(-3, 3)]
        [InlineData(12, 50)]
        [InlineData(100, 200)]
        public void GivenMinAndMaxParameters_WhenInvokeGetRandomInteger_ShouldReturnInteger(int AMin, int AMax)
        {
            // Arrange
            // Act
            var LResult = DataUtilityService.GetRandomInteger(AMin, AMax);

            // Assert
            LResult.Should().BeInRange(AMin, AMax);
        }

        [Theory]
        [InlineData(-3.3, 5.6)]
        [InlineData(10.1, 20.91)]
        [InlineData(100.5, 205.45)]
        public void GivenMinAndMaxParameters_WhenInvokeGetRandomDecimal_ShouldReturnDecimal(int AMin, int AMax)
        {
            // Arrange
            // Act
            var LResult = DataUtilityService.GetRandomDecimal(AMin, AMax);
            
            // Assert
            LResult.Should().BeInRange(AMin, AMax);
        }

        [Fact]
        public void GivenType_WhenInvokeGetRandom_ShouldReturnEnumOfGivenType()
        {
            // Arrange
            // Act
            var LResult = DataUtilityService.GetRandomEnum<DayOfWeek>();

            // Assert
            LResult.Should().NotBeNull();
            LResult.Should().BeOfType<DayOfWeek>();
        }

        [Fact]
        public void GivenNoParameters_WhenInvokeGetRandomDateTime_ShouldReturnDateTime()
        {
            // Arrange
            // Act
            var LResult = DataUtilityService.GetRandomDateTime();

            // Assert
            LResult.Should().HaveYear(2020);
            LResult.Month.Should().BeInRange(1, 12);
            LResult.Day.Should().BeInRange(1, 31);
        }
        
        [Fact]
        public void GivenMinAndMaxDateTime_WhenInvokeGetRandomDateTime_ShouldReturnDateTime()
        {
            // Arrange
            var LDateTimeMin = new DateTime(2020, 1, 1);
            var LDateTimeMax = new DateTime(2021, 10, 15);
            
            // Act
            var LResult = DataUtilityService.GetRandomDateTime(LDateTimeMin, LDateTimeMax);

            // Assert
            LResult.Year.Should().BeInRange(2020, 2021);
            LResult.Month.Should().BeInRange(1, 12);
            LResult.Day.Should().BeInRange(1, 31);
        }

        [Fact]
        public void GivenOnlyDefaultYear_WhenInvokeGetRandomDateTime_ShouldReturnDateTime()
        {
            // Arrange
            // Act
            var LResult = DataUtilityService.GetRandomDateTime(null, null, 2015);

            // Assert
            LResult.Year.Should().BeInRange(2015, 2015);
            LResult.Month.Should().BeInRange(1, 12);
            LResult.Day.Should().BeInRange(1, 31);
        }

        [Fact]
        public void GivenFlagFalse_WhenGetRandomIpAddress_ShouldReturnRandomIPv4()
        {
            // Arrange
            // Act
            var LResult = DataUtilityService.GetRandomIpAddress(false);
            
            // Assert
            LResult.AddressFamily.Should().Be(System.Net.Sockets.AddressFamily.InterNetwork);
        }

        [Fact]
        public void GivenFlagTrue_WhenGetRandomIpAddress_ShouldReturnRandomIPv6()
        {
            // Arrange
            // Act
            var LResult = DataUtilityService.GetRandomIpAddress(true);
            
            // Assert
            LResult.AddressFamily.Should().Be(System.Net.Sockets.AddressFamily.InterNetworkV6);
        }
    }
}