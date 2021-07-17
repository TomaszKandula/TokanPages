namespace TokanPages.Backend.Tests.Validators.Users
{
    using System;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Users;
    using FluentAssertions;
    using Xunit;

    public class ReAuthenticateUserCommandValidatorTest : TestBase
    {
        [Fact]
        public void GivenUserId_WhenReAuthenticateUser_ShouldSucceed()
        {
            // Arrange
            var LReAuthenticateUserCommand = new ReAuthenticateUserCommand
            {
                Id = Guid.NewGuid()
            };

            // Act
            var LReAuthenticateUserCommandValidator = new ReAuthenticateUserCommandValidator();
            var LResult = LReAuthenticateUserCommandValidator.Validate(LReAuthenticateUserCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyUserId_WhenReAuthenticateUser_ShouldThrowError()
        {
            // Arrange
            var LReAuthenticateUserCommand = new ReAuthenticateUserCommand
            {
                Id = Guid.Empty
            };

            // Act
            var LReAuthenticateUserCommandValidator = new ReAuthenticateUserCommandValidator();
            var LResult = LReAuthenticateUserCommandValidator.Validate(LReAuthenticateUserCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}