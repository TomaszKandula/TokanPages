using FluentAssertions;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Users;

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