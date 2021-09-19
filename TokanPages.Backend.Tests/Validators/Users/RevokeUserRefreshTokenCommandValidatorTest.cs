namespace TokanPages.Backend.Tests.Validators.Users
{
    using Xunit;
    using FluentAssertions;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Users;

    public class RevokeUserRefreshTokenCommandValidatorTest : TestBase
    {
        [Fact]
        public void GivenRefreshToken_WhenRevokeUserRefreshToken_ShouldFinishSuccessful()
        {
            // Arrange
            var LRevokeUserRefreshTokenCommand = new RevokeUserRefreshTokenCommand
            {
                RefreshToken = DataUtilityService.GetRandomString(100)
            };

            // Act
            var LRevokeUserRefreshTokenCommandValidator = new RevokeUserRefreshTokenCommandValidator();
            var LResult = LRevokeUserRefreshTokenCommandValidator.Validate(LRevokeUserRefreshTokenCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenNoRefreshToken_WhenRevokeUserRefreshToken_ShouldThrowError()
        {
            // Arrange
            var LRevokeUserRefreshTokenCommand = new RevokeUserRefreshTokenCommand
            {
                RefreshToken = string.Empty
            };

            // Act
            var LRevokeUserRefreshTokenCommandValidator = new RevokeUserRefreshTokenCommandValidator();
            var LResult = LRevokeUserRefreshTokenCommandValidator.Validate(LRevokeUserRefreshTokenCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}