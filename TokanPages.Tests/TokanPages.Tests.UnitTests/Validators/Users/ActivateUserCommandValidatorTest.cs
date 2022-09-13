using System;
using FluentAssertions;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Users;

public class ActivateUserCommandValidatorTest
{
    [Fact]
    public void GivenActivationId_WhenActivateUser_ShouldSucceed() 
    {
        // Arrange
        var command = new ActivateUserCommand { ActivationId = Guid.NewGuid() };

        // Act
        var validator = new ActivateUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyActivationId_WhenActivateUser_ShouldThrowError() 
    {
        // Arrange
        var command = new ActivateUserCommand { ActivationId = Guid.Empty };

        // Act
        var validator = new ActivateUserCommandValidator();
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}