namespace TokanPages.Tests.UnitTests.Validators.Users;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Users;

public class RemoveUserCommandValidatorTest
{
    [Fact]
    public void GivenCorrectId_WhenRemoveUser_ShouldFinishSuccessful()
    {
        // Arrange
        var removeUserCommand = new RemoveUserCommand
        {
            Id = Guid.NewGuid()
        };

        // Act
        var validator = new RemoveUserCommandValidator();
        var result = validator.Validate(removeUserCommand);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyId_WhenRemoveUser_ShouldThrowError()
    {
        // Arrange
        var removeUserCommand = new RemoveUserCommand
        {
            Id = Guid.Empty
        };

        // Act
        var validator = new RemoveUserCommandValidator();
        var result = validator.Validate(removeUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}