using FluentAssertions;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Users;

public class AddUserNoteCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenUserNote_WhenAddUserNoteCommand_ShouldSucceed()
    {
        // Arrange
        var command = new AddUserNoteCommand
        {
            Note = DataUtilityService.GetRandomString(50)
        };

        var mockedConfiguration = GetMockSettings();
        var validator = new AddUserNoteCommandValidator(mockedConfiguration.Object);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenTooLargeUserNote_WhenAddUserNoteCommand_ShouldThrowError()
    {
        // Arrange
        var command = new AddUserNoteCommand
        {
            Note = DataUtilityService.GetRandomString(2048)
        };

        var mockedConfiguration = GetMockSettings();
        var validator = new AddUserNoteCommandValidator(mockedConfiguration.Object);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.TOO_LONG_USER_NOTE));
    }

    [Fact]
    public void GivenEmptyLargeUserNote_WhenAddUserNoteCommand_ShouldThrowError()
    {
        // Arrange
        var command = new AddUserNoteCommand
        {
            Note = string.Empty
        };

        var mockedConfiguration = GetMockSettings();
        var validator = new AddUserNoteCommandValidator(mockedConfiguration.Object);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}