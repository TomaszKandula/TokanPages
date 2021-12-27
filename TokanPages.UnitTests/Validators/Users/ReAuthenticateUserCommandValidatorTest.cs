namespace TokanPages.UnitTests.Validators.Users;

using Xunit;
using FluentAssertions;
using System;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Users;

public class ReAuthenticateUserCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenUserId_WhenReAuthenticateUser_ShouldSucceed()
    {
        // Arrange
        var reAuthenticateUserCommand = new ReAuthenticateUserCommand
        {
            Id = Guid.NewGuid()
        };

        // Act
        var reAuthenticateUserCommandValidator = new ReAuthenticateUserCommandValidator();
        var result = reAuthenticateUserCommandValidator.Validate(reAuthenticateUserCommand);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyUserId_WhenReAuthenticateUser_ShouldThrowError()
    {
        // Arrange
        var reAuthenticateUserCommand = new ReAuthenticateUserCommand
        {
            Id = Guid.Empty
        };

        // Act
        var reAuthenticateUserCommandValidator = new ReAuthenticateUserCommandValidator();
        var result = reAuthenticateUserCommandValidator.Validate(reAuthenticateUserCommand);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}