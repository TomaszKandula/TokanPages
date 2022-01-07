namespace TokanPages.Tests.UnitTests.Services;

using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Backend.Domain.Enums;
using Backend.Database.Initializer.Data.Users;
    
public class JwtUtilityServiceTest : TestBase
{
    [Fact]
    public void GivenClaims_WhenInvokeGenerateJwt_ShouldReturnValidJwt()
    {
        // Arrange
        const string claimTypesName = "unique_name";
        const string claimTypesRole = "role";
        const string claimTypesNameIdentifier = "nameid";
        const string claimTypesGivenName = "given_name";
        const string claimTypesSurname = "family_name";
        const string claimTypesEmail = "email";

        const string webSecret = "0e723112-72e2-43fc-a348-ddb0147554f5";
        const string issuer = "www.jwt-issuer.com";
        const string audience = "www.some-api.com";

        var userAlias = DataUtilityService.GetRandomString();
        var tokenExpires = DateTime.Now.AddDays(30);
        var getValidClaims = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, userAlias),
            new Claim(ClaimTypes.Role, nameof(Roles.EverydayUser)),
            new Claim(ClaimTypes.NameIdentifier, User1.Id.ToString()),
            new Claim(ClaimTypes.GivenName, User1.FirstName),
            new Claim(ClaimTypes.Surname, User1.LastName),
            new Claim(ClaimTypes.Email, User1.EmailAddress)
        });
            
        // Act
        var jwt = JwtUtilityService.GenerateJwt(tokenExpires, getValidClaims, webSecret, issuer, audience);
            
        // Assert
        jwt.Should().NotBeNullOrEmpty();

        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(jwt);
        jsonToken.Should().NotBeNull();
        jsonToken.Issuer.Should().Be(issuer);
            
        var securityToken = jsonToken as JwtSecurityToken;
        securityToken.Should().NotBeNull();
        securityToken?.Claims.First(claim => claim.Type == claimTypesName).Value.Should().Be(userAlias);
        securityToken?.Claims.First(claim => claim.Type == claimTypesRole).Value.Should().Be(nameof(Roles.EverydayUser));
        securityToken?.Claims.First(claim => claim.Type == claimTypesNameIdentifier).Value.Should().Be(User1.Id.ToString());
        securityToken?.Claims.First(claim => claim.Type == claimTypesGivenName).Value.Should().Be(User1.FirstName);
        securityToken?.Claims.First(claim => claim.Type == claimTypesSurname).Value.Should().Be(User1.LastName);
        securityToken?.Claims.First(claim => claim.Type == claimTypesEmail).Value.Should().Be(User1.EmailAddress);
    }

    [Theory]
    [InlineData(15)]
    [InlineData(7)]
    [InlineData(-7)]
    public void GivenExpirationTime_WhenInvokeGenerateRefreshToken_ShouldReturnNewToken(int expiresIn)
    {
        // Arrange
        const string ipAddress = "127.0.0.1";
            
        // Act
        var result = JwtUtilityService.GenerateRefreshToken(ipAddress, expiresIn);

        // Assert
        result.Token.Should().HaveLength(344);
        result.CreatedByIp.Should().Be(ipAddress);
    }
        
    [Fact]
    public void GivenZeroMinutesToExpire_WhenInvokeGenerateRefreshToken_ShouldThrowError()
    {
        // Arrange
        const string ipAddress = "127.0.0.1";
            
        // Act
        // Assert
        Assert.Throws<ArgumentException>(() => JwtUtilityService.GenerateRefreshToken(ipAddress, 0));
    }
}