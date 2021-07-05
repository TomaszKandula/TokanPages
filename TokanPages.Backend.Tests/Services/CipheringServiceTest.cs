using Xunit;
using FluentAssertions;
using TokanPages.Backend.Cqrs.Services.CipheringService;
using TokanPages.Backend.Shared.Services.DataProviderService;

namespace TokanPages.Backend.Tests.Services
{
    public class CipheringServiceTest
    {
        private readonly DataProviderService FDataProviderService;
        
        public CipheringServiceTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public void GivenPlainTextPassword_WhenInvokeGetHashedPassword_ShouldReturnHashedPassword()
        {
            // Arrange
            const int CIPHER_LOG_ROUNDS = 12;
            var LPlainTextPassword = FDataProviderService.GetRandomString();
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