namespace TokanPages.Backend.Tests.Services
{
    using Cqrs.Services.CipheringService;
    using FluentAssertions;
    using Xunit;

    public class CipheringServiceTest : TestBase
    {

        [Fact]
        public void GivenPlainTextPassword_WhenInvokeGetHashedPassword_ShouldReturnHashedPassword()
        {
            // Arrange
            const int cipherLogRounds = 12;
            var plainTextPassword = DataUtilityService.GetRandomString();
            var cipher = new CipheringService();

            // Act
            var salt = cipher.GenerateSalt(cipherLogRounds);
            var hashed = cipher.GetHashedPassword(plainTextPassword, salt);

            // Assert
            hashed.Should().NotBeNullOrEmpty();
            var verify = cipher.VerifyPassword(plainTextPassword, hashed);
            verify.Should().BeTrue();
        }
    }
}