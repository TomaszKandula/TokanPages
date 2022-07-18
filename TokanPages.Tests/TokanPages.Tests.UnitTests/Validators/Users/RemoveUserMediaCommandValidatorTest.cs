namespace TokanPages.Tests.UnitTests.Validators.Users;

using Xunit;
using FluentAssertions;
using Backend.Shared.Resources;
using Backend.Cqrs.Handlers.Commands.Users;

public class RemoveUserMediaCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenValidInput_WhenRemoveUserMedia_ShouldSucceed()
    {
        // Arrange
        var command = new RemoveUserMediaCommand { UniqueBlobName = DataUtilityService.GetRandomString() };
        var validator = new RemoveUserMediaCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenEmptyInput_WhenRemoveUserMedia_ShouldThrowError()
    {
        // Arrange
        var command = new RemoveUserMediaCommand();
        var validator = new RemoveUserMediaCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }

    [Fact]
    public void GivenTooLongString_WhenRemoveUserMedia_ShouldThrowError()
    {
        // Arrange
        var command = new RemoveUserMediaCommand { UniqueBlobName = DataUtilityService.GetRandomString(2148) };
        var validator = new RemoveUserMediaCommandValidator();

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.NAME_TOO_LONG));
    }
}