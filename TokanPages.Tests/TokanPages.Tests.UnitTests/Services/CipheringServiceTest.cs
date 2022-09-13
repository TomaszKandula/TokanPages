using FluentAssertions;
using TokanPages.Services.CipheringService;
using Xunit;

namespace TokanPages.Tests.UnitTests.Services;

public class CipheringServiceTest : TestBase
{
    [Fact]
    public void GivenValidInputs_WhenInvokeGetHashedPassword_ShouldSucceed()
    {
        // Arrange
        const int cipherLogRounds = 12;
        var plainTextPassword = DataUtilityService.GetRandomString();
        var cipher = new CipheringService();

        // Act
        var hashed = cipher.GetHashedPassword(plainTextPassword, cipher.GenerateSalt(cipherLogRounds));

        // Assert
        hashed.Should().NotBeNullOrEmpty();
        cipher.VerifyPassword(plainTextPassword, hashed).Should().BeTrue();
    }
}