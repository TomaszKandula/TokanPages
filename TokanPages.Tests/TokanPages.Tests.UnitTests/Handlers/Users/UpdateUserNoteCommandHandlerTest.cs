using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class UpdateUserNoteCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenUserNote_WhenUpdateUserNoteCommand_ShouldSucceed()
    {
        // Arrange
        const string currentNote = "This is my test existing note...";
        var compressedNote = currentNote.CompressToBase64();

        var userId = Guid.NewGuid();
        var user = new Backend.Domain.Entities.User.Users
        {
            Id = userId,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var userNoteId = Guid.NewGuid();
        var note = new Backend.Domain.Entities.User.UserNote
        {
            Id = userNoteId,
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

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var command = new UpdateUserNoteCommand
        {
            Id = userNoteId,
            Note = "This is new user note..."
        };

        var handler = new UpdateUserNoteCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedUserService.Object, 
            mockedDateTimeService.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task GivenInvalidUserNoteId_WhenUpdateUserNoteCommand_ShouldThrowError()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var user = new Backend.Domain.Entities.User.Users
        {
            Id = userId,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();
        var mockedDateTimeService = new Mock<IDateTimeService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var command = new UpdateUserNoteCommand
        {
            Id = Guid.NewGuid(),
            Note = "This is new user note..."
        };

        var handler = new UpdateUserNoteCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedUserService.Object, 
            mockedDateTimeService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.CANNOT_FIND_USER_NOTE));
    }
}