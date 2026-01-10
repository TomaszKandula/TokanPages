using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Enums;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class AddUserNoteCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenUserNote_WhenAddUserNoteCommand_ShouldSucceed()
    {
        // Arrange
        const string currentNote = "This is my test existing note...";
        var limitNotes = SetReturnValue("10");
        var compressedNote = currentNote.CompressToBase64();
        var userId = Guid.NewGuid();

        var user = new Backend.Domain.Entities.User.User
        {
            Id = userId,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var note = new Backend.Domain.Entities.User.UserNote
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Note = compressedNote,
            CreatedBy = userId,
            CreatedAt = DataUtilityService.GetRandomDateTime()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.UserNotes.AddAsync(note);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfiguration = new Mock<IConfiguration>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(user.Id);

        mockedConfiguration
            .Setup(configuration => configuration.GetSection(It.IsAny<string>()))
            .Returns(limitNotes);

        var command = new AddUserNoteCommand
        {
            Note = "This is new user note..."
        };

        var handler = new AddUserNoteCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedUserService.Object, 
            mockedDateTimeService.Object,
            mockedConfiguration.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.CurrentNotes.Should().Be(2);
        result.Result.Should().Be(UserNote.NoteAdded);
    }

    [Fact]
    public async Task GivenTooManyUserNotes_WhenAddUserNoteCommand_ShouldRejectNote()
    {
        // Arrange
        const string currentNote = "This is my test existing note...";
        var limitNotes = SetReturnValue("1");
        var compressedNote = currentNote.CompressToBase64();
        var userId = Guid.NewGuid();

        var user = new Backend.Domain.Entities.User.User
        {
            Id = userId,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var note = new Backend.Domain.Entities.User.UserNote
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Note = compressedNote,
            CreatedBy = userId,
            CreatedAt = DataUtilityService.GetRandomDateTime()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.UserNotes.AddAsync(note);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();
        var mockedConfiguration = new Mock<IConfiguration>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(user.Id);

        mockedConfiguration
            .Setup(configuration => configuration.GetSection(It.IsAny<string>()))
            .Returns(limitNotes);

        var command = new AddUserNoteCommand
        {
            Note = "This is new user note..."
        };

        var handler = new AddUserNoteCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedUserService.Object, 
            mockedDateTimeService.Object,
            mockedConfiguration.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.CurrentNotes.Should().Be(1);
        result.Result.Should().Be(UserNote.NoteRejected);
    }
}