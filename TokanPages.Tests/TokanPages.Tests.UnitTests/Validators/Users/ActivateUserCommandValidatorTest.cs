namespace TokanPages.Tests.UnitTests.Validators.Users;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Users;

public class ActivateUserCommandValidatorTest
{
    [Fact]
    public void GivenActivationId_WhenActivateUser_ShouldFinishSuccessful() 
    {
        // Arrange
        var activateUserCommand = new ActivateUserCommand 
        { 
            ActivationId = Guid.NewGuid()
        };

        // Act
        var activateUserCommandValidator = new ActivateUserCommandValidator();
        var result = activateUserCommandValidator.Validate(activateUserCommand);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyActivationId_WhenActivateUser_ShouldThrowError() 
    {
        // Arrange
        var activateUserCommand = new ActivateUserCommand 
        { 
            ActivationId = Guid.Empty
        };

        // Act
        var activateUserCommandValidator = new ActivateUserCommandValidator();
        var result = activateUserCommandValidator.Validate(activateUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}