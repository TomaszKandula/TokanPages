using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Shared.Options;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using Xunit;
using UserNote = TokanPages.Backend.Domain.Entities.Users.UserNote;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class AddUserNoteCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenUserNote_WhenAddUserNoteCommand_ShouldSucceed()
    {
        // Arrange
        const string currentNote = "This is my test existing note...";
        var compressedNote = currentNote.CompressToBase64();
        var userId = Guid.NewGuid();

        var user = new User
        {
            Id = userId,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString(),
            ResetId = null,
            CreatedBy = Guid.NewGuid(),
            CreatedAt = default,
            IsVerified = false,
            IsDeleted = false,
            HasBusinessLock = false
        };

        var note = new UserNote
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
        var mockedConfiguration = GetMockSettings();

        mockedUserService
            .Setup(service => service.GetActiveUser(It.IsAny<Guid?>()))
            .ReturnsAsync(user);

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(user.Id);

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
        result.Result.Should().Be(Backend.Domain.Enums.UserNote.NoteAdded);
    }

    [Fact]
    public async Task GivenTooManyUserNotes_WhenAddUserNoteCommand_ShouldRejectNote()
    {
        // Arrange
        const string currentNote = "This is my test existing note...";
        const int limitNotes = 1;
        var compressedNote = currentNote.CompressToBase64();
        var userId = Guid.NewGuid();

        var user = new User
        {
            Id = userId,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString(),
            ResetId = null,
            CreatedBy = Guid.NewGuid(),
            CreatedAt = default,
            IsVerified = false,
            IsDeleted = false,
            HasBusinessLock = false
        };

        var note = new UserNote
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
        var mockedConfiguration = GetMockSettings();

        mockedUserService
            .Setup(service => service.GetActiveUser(It.IsAny<Guid?>()))
            .ReturnsAsync(user);

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(user.Id);

        mockedConfiguration
            .Setup(configuration => configuration.Value)
            .Returns(new AppSettingsModel
            {
                UserNoteMaxCount = limitNotes
            });

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
        result.Result.Should().Be(Backend.Domain.Enums.UserNote.NoteRejected);
    }
}