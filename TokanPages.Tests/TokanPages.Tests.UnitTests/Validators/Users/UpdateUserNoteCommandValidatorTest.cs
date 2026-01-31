using FluentAssertions;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Shared.Resources;
using Xunit;

namespace TokanPages.Tests.UnitTests.Validators.Users;

public class UpdateUserNoteCommandValidatorTest : TestBase
{
    [Fact]
    public void GivenUserNote_WhenUpdateUserNoteCommand_ShouldSucceed()
    {
        // Arrange
        const int noteLength = 50;
        var command = new UpdateUserNoteCommand
        {
            Note = DataUtilityService.GetRandomString(noteLength)
        };

        var mockedConfiguration = GetMockSettings();
        var validator = new UpdateUserNoteCommandValidator(mockedConfiguration.Object);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void GivenTooLargeUserNote_WhenUpdateUserNoteCommand_ShouldThrowError()
    {
        // Arrange
        const int noteLength = 2048;

        var command = new UpdateUserNoteCommand
        {
            Note = DataUtilityService.GetRandomString(noteLength)
        };

        var mockedConfiguration = GetMockSettings();
        var validator = new UpdateUserNoteCommandValidator(mockedConfiguration.Object);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.TOO_LONG_USER_NOTE));
    }

    [Fact]
    public void GivenEmptyLargeUserNote_WhenUpdateUserNoteCommand_ShouldThrowError()
    {
        // Arrange
        var command = new UpdateUserNoteCommand
        {
            Note = string.Empty
        };

        var mockedConfiguration = GetMockSettings();
        var validator = new UpdateUserNoteCommandValidator(mockedConfiguration.Object);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}