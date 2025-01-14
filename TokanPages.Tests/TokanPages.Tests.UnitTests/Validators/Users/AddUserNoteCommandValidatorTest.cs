using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
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
        const int noteLength = 50;
        var noteMaxSize = SetReturnValue("100");

        var command = new AddUserNoteCommand
        {
            Note = DataUtilityService.GetRandomString(noteLength)
        };

        var mockedConfiguration = new Mock<IConfiguration>();
        mockedConfiguration
            .Setup(configuration => configuration.GetSection(It.IsAny<string>()))
            .Returns(noteMaxSize);

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
        const int noteLength = 200;
        var noteMaxSize = SetReturnValue("100");

        var command = new AddUserNoteCommand
        {
            Note = DataUtilityService.GetRandomString(noteLength)
        };

        var mockedConfiguration = new Mock<IConfiguration>();
        mockedConfiguration
            .Setup(configuration => configuration.GetSection(It.IsAny<string>()))
            .Returns(noteMaxSize);

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
        var noteMaxSize = SetReturnValue("100");
        var command = new AddUserNoteCommand
        {
            Note = string.Empty
        };

        var mockedConfiguration = new Mock<IConfiguration>();
        mockedConfiguration
            .Setup(configuration => configuration.GetSection(It.IsAny<string>()))
            .Returns(noteMaxSize);

        var validator = new AddUserNoteCommandValidator(mockedConfiguration.Object);

        // Act
        var result = validator.Validate(command);

        // Assert
        result.Errors.Count.Should().Be(1);
        result.Errors[0].ErrorCode.Should().Be(nameof(ValidationCodes.REQUIRED));
    }
}