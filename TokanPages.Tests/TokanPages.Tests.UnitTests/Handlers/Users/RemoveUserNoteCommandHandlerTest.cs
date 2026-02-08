using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Users.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Domain.Entities.Users;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Services.UserService.Abstractions;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class RemoveUserNoteCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenNoteId_WhenRemoveUserNote_ShouldSucceed()
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

        var userNoteId = Guid.NewGuid();
        var note = new UserNote
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

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        mockedUserService
            .Setup(service => service.GetLoggedUserId())
            .Returns(user.Id);

        var command = new RemoveUserNoteCommand
        {
            Id = userNoteId
        };

        var handler = new RemoveUserNoteCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedUserService.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task GivenWrongUserNoteId_WhenRemoveUserNote_ShouldThrowError()
    {
        // Arrange
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

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();

        mockedUserService
            .Setup(service => service.GetActiveUser(
                It.IsAny<Guid?>(), 
                It.IsAny<bool>(), 
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        var command = new RemoveUserNoteCommand
        {
            Id = Guid.NewGuid()
        };

        var handler = new RemoveUserNoteCommandHandler(
            databaseContext,
            mockedLogger.Object,
            mockedUserService.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.CANNOT_FIND_USER_NOTE));
    }
}