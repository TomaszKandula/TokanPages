using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using TokanPages.Backend.Identity.Authorization;
using TokanPages.Backend.Database.Initializer.Data.Users;
using TokanPages.Backend.Shared.Services.DataProviderService;

namespace TokanPages.Backend.Tests.Services
{
    public class DataProviderServiceTest
    {
        private readonly DataProviderService FDataProviderService;

        public DataProviderServiceTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public void GivenNoParameters_WhenInvokeGetRandomString_ShouldReturnTwelveChars()
        {
            // Arrange 
            // Act
            var LResult = FDataProviderService.GetRandomString();

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
            var LResult = FDataProviderService.GetRandomString(LENGTH, PREFIX);

            // Assert
            LResult.Length.Should().Be(LENGTH + LPrefixLength);
            LResult.Should().Contain(PREFIX);
        }

        [Fact]
        public void GivenNoParameters_WhenInvokeGetRandomEmail_ShouldReturnGmailEmailAddress()
        {
            // Arrange
            // Act
            var LResult = FDataProviderService.GetRandomEmail();

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
            var LResult = FDataProviderService.GetRandomEmail(LENGTH, DOMAIN);

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
            var LResult = FDataProviderService.GetRandomStream();

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
            var LResult = FDataProviderService.GetRandomStream(SIZE_IN_KB);

            // Assert
            LResult.Should().NotBeNull();
            LResult.Length.Should().Be(EXPECTED_LENGTH);
        }

        [Fact]
        public void GivenNoParameters_WhenInvokeGetRandomInteger_ShouldReturnInteger()
        {
            // Arrange
            // Act
            var LResult = FDataProviderService.GetRandomInteger();

            // Assert
            LResult.Should().BeInRange(0, 12);
        }

        [Fact]
        public void GivenNoParameters_WhenInvokeGetRandomDecimal_ShouldReturnDecimal()
        {
            // Arrange
            // Act
            var LResult = FDataProviderService.GetRandomDecimal();

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
            var LResult = FDataProviderService.GetRandomInteger(AMin, AMax);

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
            var LResult = FDataProviderService.GetRandomDecimal(AMin, AMax);
            
            // Assert
            LResult.Should().BeInRange(AMin, AMax);
        }

        [Fact]
        public void GivenType_WhenInvokeGetRandom_ShouldReturnEnumOfGivenType()
        {
            // Arrange
            // Act
            var LResult = FDataProviderService.GetRandomEnum<DayOfWeek>();

            // Assert
            LResult.Should().NotBeNull();
            LResult.Should().BeOfType<DayOfWeek>();
        }

        [Fact]
        public void GivenNoParameters_WhenInvokeGetRandomDateTime_ShouldReturnDateTime()
        {
            // Arrange
            // Act
            var LResult = FDataProviderService.GetRandomDateTime();

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
            var LResult = FDataProviderService.GetRandomDateTime(LDateTimeMin, LDateTimeMax);

            // Assert
            LResult.Year.Should().BeInRange(2020, 2021);
            LResult.Month.Should().BeInRange(1, 12);
            LResult.Day.Should().BeInRange(1, 31);
        }

        [Fact]
        public void GivenClaims_WhenInvokeGenerateJwt_ShouldReturnValidJwt()
        {
            // Arrange
            const string WEB_SECRET = "0e723112-72e2-43fc-a348-ddb0147554f5";
            const string ISSUER = "www.jwt-issuer.com";
            const string AUDIENCE = "www.some-api.com";

            var LUserAlias = FDataProviderService.GetRandomString();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LGetValidClaims = new ClaimsIdentity(new[]
            {
                new Claim(Claims.UserAlias, LUserAlias),
                new Claim(Claims.Roles, Roles.EverydayUser),
                new Claim(Claims.UserId, User1.FId.ToString()),
                new Claim(Claims.FirstName, User1.FIRST_NAME),
                new Claim(Claims.LastName, User1.LAST_NAME),
                new Claim(Claims.EmailAddress, User1.EMAIL_ADDRESS)
            });
            
            // Act
            var LJwt = FDataProviderService.GenerateJwt(LTokenExpires, LGetValidClaims, WEB_SECRET, ISSUER, AUDIENCE);
            
            // Assert
            LJwt.Should().NotBeNullOrEmpty();

            var LHandler = new JwtSecurityTokenHandler();
            var LJsonToken = LHandler.ReadToken(LJwt);
            LJsonToken.Should().NotBeNull();
            LJsonToken.Issuer.Should().Be(ISSUER);
            
            var LSecurityToken = LJsonToken as JwtSecurityToken;
            LSecurityToken.Should().NotBeNull();
            LSecurityToken?.Claims.First(AClaim => AClaim.Type == Claims.UserAlias).Value.Should().Be(LUserAlias);
            LSecurityToken?.Claims.First(AClaim => AClaim.Type == Claims.Roles).Value.Should().Be(Roles.EverydayUser);
            LSecurityToken?.Claims.First(AClaim => AClaim.Type == Claims.UserId).Value.Should().Be(User1.FId.ToString());
            LSecurityToken?.Claims.First(AClaim => AClaim.Type == Claims.FirstName).Value.Should().Be(User1.FIRST_NAME);
            LSecurityToken?.Claims.First(AClaim => AClaim.Type == Claims.LastName).Value.Should().Be(User1.LAST_NAME);
            LSecurityToken?.Claims.First(AClaim => AClaim.Type == Claims.EmailAddress).Value.Should().Be(User1.EMAIL_ADDRESS);
        }
    }
}