using Xunit;
using FluentAssertions;
using TokanPages.Backend.Cqrs.Services.Cipher;
using TokanPages.Backend.Core.Services.DataProviderService;

namespace TokanPages.Backend.Tests.Services
{
    public class CipherServiceTest
    {
        private readonly DataProviderService FDataProviderService;
        
        public CipherServiceTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public void GivenPlainTextPassword_WhenInvokeGetHashedPassword_ShouldReturnHashedPassword()
        {
            // Arrange
            const int CIPHER_LOG_ROUNDS = 12;
            var LPlainTextPassword = FDataProviderService.GetRandomString();
            var LCipher = new Cipher();

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