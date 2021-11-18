namespace TokanPages.UnitTests.Validators.Users
{
    using Xunit;
    using FluentAssertions;
    using Backend.Shared.Resources;
    using Backend.Cqrs.Handlers.Commands.Users;

    public class RevokeUserRefreshTokenCommandValidatorTest : TestBase
    {
        [Fact]
        public void GivenRefreshToken_WhenRevokeUserRefreshToken_ShouldFinishSuccessful()
        {
            // Arrange
            var revokeUserRefreshTokenCommand = new RevokeUserRefreshTokenCommand
            {
                RefreshToken = DataUtilityService.GetRandomString(100)
            };

            // Act
            var revokeUserRefreshTokenCommandValidator = new RevokeUserRefreshTokenCommandValidator();
            var result = revokeUserRefreshTokenCommandValidator.Validate(revokeUserRefreshTokenCommand);

            // Assert
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenNoRefreshToken_WhenRevokeUserRefreshToken_ShouldThrowError()
        {
            // Arrange
            var revokeUserRefreshTokenCommand = new RevokeUserRefreshTokenCommand
            {
                RefreshToken = string.Empty
            };

            // Act
            var revokeUserRefreshTokenCommandValidator = new RevokeUserRefreshTokenCommandValidator();
            var result = revokeUserRefreshTokenCommandValidator.Validate(revokeUserRefreshTokenCommand);

            // Assert
            result.Errors.Count.Should().Be(1);
            result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}