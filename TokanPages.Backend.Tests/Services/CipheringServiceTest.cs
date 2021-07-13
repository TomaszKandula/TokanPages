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
            const int CIPHER_LOG_ROUNDS = 12;
            var LPlainTextPassword = DataUtilityService.GetRandomString();
            var LCipher = new CipheringService();

            // Act
            var LSalt = LCipher.GenerateSalt(CIPHER_LOG_ROUNDS);
            var LHashed = LCipher.GetHashedPassword(LPlainTextPassword, LSalt);

            // Assert
            LHashed.Should().NotBeNullOrEmpty();
            var LVerify = LCipher.VerifyPassword(LPlainTextPassword, LHashed);
            LVerify.Should().BeTrue();
        }
    }
}