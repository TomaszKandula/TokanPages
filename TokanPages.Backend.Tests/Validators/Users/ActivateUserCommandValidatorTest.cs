namespace TokanPages.Backend.Tests.Validators.Users
{
    using Xunit;
    using System;
    using FluentAssertions;
    using Shared.Resources;
    using Cqrs.Handlers.Commands.Users;

    public class ActivateUserCommandValidatorTest
    {
        [Fact]
        public void GivenActivationId_WhenActivateUser_ShouldFinishSuccessful() 
        {
            // Arrange
            var LActivateUserCommand = new ActivateUserCommand 
            { 
                ActivationId = Guid.NewGuid()
            };

            // Act
            var LActivateUserCommandValidator = new ActivateUserCommandValidator();
            var LResults = LActivateUserCommandValidator.Validate(LActivateUserCommand);

            // Assert
            LResults.Errors.Should().BeEmpty();
        }

        [Fact]
        public void GivenEmptyActivationId_WhenActivateUser_ShouldThrowError() 
        {
            // Arrange
            var LActivateUserCommand = new ActivateUserCommand 
            { 
                ActivationId = Guid.Empty
            };

            // Act
            var LActivateUserCommandValidator = new ActivateUserCommandValidator();
            var LResults = LActivateUserCommandValidator.Validate(LActivateUserCommand);

            // Assert
            LResults.Errors.Count.Should().Be(1);
            LResults.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}