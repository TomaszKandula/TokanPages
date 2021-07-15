namespace TokanPages.Backend.Tests.Services
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.IdentityModel.Tokens.Jwt;
    using Identity.Authorization;
    using Database.Initializer.Data.Users;
    using FluentAssertions;
    using Xunit;
    
    public class JwtUtilityServiceTest : TestBase
    {
        [Fact]
        public void GivenClaims_WhenInvokeGenerateJwt_ShouldReturnValidJwt()
        {
            // Arrange
            const string CLAIM_TYPES_NAME = "unique_name";
            const string CLAIM_TYPES_ROLE = "role";
            const string CLAIM_TYPES_NAME_IDENTIFIER = "nameid";
            const string CLAIM_TYPES_GIVEN_NAME = "given_name";
            const string CLAIM_TYPES_SURNAME = "family_name";
            const string CLAIM_TYPES_EMAIL = "email";

            const string WEB_SECRET = "0e723112-72e2-43fc-a348-ddb0147554f5";
            const string ISSUER = "www.jwt-issuer.com";
            const string AUDIENCE = "www.some-api.com";

            var LUserAlias = DataUtilityService.GetRandomString();
            var LTokenExpires = DateTime.Now.AddDays(30);
            var LGetValidClaims = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, LUserAlias),
                new Claim(ClaimTypes.Role, nameof(Roles.EverydayUser)),
                new Claim(ClaimTypes.NameIdentifier, User1.FId.ToString()),
                new Claim(ClaimTypes.GivenName, User1.FIRST_NAME),
                new Claim(ClaimTypes.Surname, User1.LAST_NAME),
                new Claim(ClaimTypes.Email, User1.EMAIL_ADDRESS)
            });
            
            // Act
            var LJwt = JwtUtilityService.GenerateJwt(LTokenExpires, LGetValidClaims, WEB_SECRET, ISSUER, AUDIENCE);
            
            // Assert
            LJwt.Should().NotBeNullOrEmpty();

            var LHandler = new JwtSecurityTokenHandler();
            var LJsonToken = LHandler.ReadToken(LJwt);
            LJsonToken.Should().NotBeNull();
            LJsonToken.Issuer.Should().Be(ISSUER);
            
            var LSecurityToken = LJsonToken as JwtSecurityToken;
            LSecurityToken.Should().NotBeNull();
            LSecurityToken?.Claims.First(AClaim => AClaim.Type == CLAIM_TYPES_NAME).Value.Should().Be(LUserAlias);
            LSecurityToken?.Claims.First(AClaim => AClaim.Type == CLAIM_TYPES_ROLE).Value.Should().Be(nameof(Roles.EverydayUser));
            LSecurityToken?.Claims.First(AClaim => AClaim.Type == CLAIM_TYPES_NAME_IDENTIFIER).Value.Should().Be(User1.FId.ToString());
            LSecurityToken?.Claims.First(AClaim => AClaim.Type == CLAIM_TYPES_GIVEN_NAME).Value.Should().Be(User1.FIRST_NAME);
            LSecurityToken?.Claims.First(AClaim => AClaim.Type == CLAIM_TYPES_SURNAME).Value.Should().Be(User1.LAST_NAME);
            LSecurityToken?.Claims.First(AClaim => AClaim.Type == CLAIM_TYPES_EMAIL).Value.Should().Be(User1.EMAIL_ADDRESS);
        }

        [Theory]
        [InlineData(15)]
        [InlineData(7)]
        [InlineData(-7)]
        public void GivenExpirationTime_WhenInvokeGenerateRefreshToken_ShouldReturnNewToken(int AExpiresIn)
        {
            // Arrange
            const string IP_ADDRESS = "127.0.0.1";
            
            // Act
            var LResult = JwtUtilityService.GenerateRefreshToken(IP_ADDRESS, AExpiresIn);

            // Assert
            LResult.Token.Should().HaveLength(344);
            LResult.CreatedByIp.Should().Be(IP_ADDRESS);
        }
        
        [Fact]
        public void GivenZeroMinutesToExpire_WhenInvokeGenerateRefreshToken_ShouldThrowError()
        {
            // Arrange
            const string IP_ADDRESS = "127.0.0.1";
            
            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => JwtUtilityService.GenerateRefreshToken(IP_ADDRESS, 0));
        }
    }
}