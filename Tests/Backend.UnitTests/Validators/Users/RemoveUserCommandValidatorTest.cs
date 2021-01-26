using Xunit;
using FluentAssertions;
using System;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Users;

namespace Backend.UnitTests.Validators.Users
{
    public class RemoveUserCommandValidatorTest
    {
        [Fact]
        public void RemoveUser_WhenIdIsCorrect_ShouldFinishSuccessfull()
        {
            // Arrange
            var LRemoveUserCommand = new RemoveUserCommand
            {
                Id = Guid.NewGuid()
            };

            // Act
            var LValidator = new RemoveUserCommandValidator();
            var LResult = LValidator.Validate(LRemoveUserCommand);

            // Assert
            LResult.Errors.Should().BeEmpty();
        }

        [Fact]
        public void RemoveUser_WhenIdIsEmpty_ShouldThrowError()
        {
            // Arrange
            var LRemoveUserCommand = new RemoveUserCommand
            {
                Id = Guid.Empty
            };

            // Act
            var LValidator = new RemoveUserCommandValidator();
            var LResult = LValidator.Validate(LRemoveUserCommand);

            // Assert
            LResult.Errors.Count.Should().Be(1);
            LResult.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
        }
    }
}
