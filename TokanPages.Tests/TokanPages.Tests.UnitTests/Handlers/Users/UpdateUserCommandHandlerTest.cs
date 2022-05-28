namespace TokanPages.Tests.UnitTests.Handlers.Users;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Backend.Domain.Entities;
using Backend.Core.Exceptions;
using TokanPages.Services.UserService;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Handlers.Commands.Users;
using Backend.Core.Utilities.DateTimeService;

public class UpdateUserCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectId_WhenUpdateUser_ShouldUpdateEntity()
    {
        // Arrange
        var user = new Users
        {
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            LastLogged = null,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(user);
        await databaseContext.SaveChangesAsync();

        var updateUserCommand = new UpdateUserCommand
        {
            Id = user.Id,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
        };

        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();
        var updateUserCommandHandler = new UpdateUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object, 
            mockedUserService.Object);

        // Act
        await updateUserCommandHandler.Handle(updateUserCommand, CancellationToken.None);

        // Assert
        var userEntity = await databaseContext.Users.FindAsync(updateUserCommand.Id);

        userEntity.Should().NotBeNull();
        userEntity.EmailAddress.Should().Be(updateUserCommand.EmailAddress);
        userEntity.UserAlias.Should().Be(updateUserCommand.UserAlias);
        userEntity.IsActivated.Should().BeTrue();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenUpdateUser_ShouldThrowError()
    {
        // Arrange
        var databaseContext = GetTestDatabaseContext();
        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();
        var updateUserCommandHandler = new UpdateUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object, 
            mockedUserService.Object);

        var updateUserCommand = new UpdateUserCommand
        {
            Id = Guid.Parse("1edb4c7d-8cf0-4811-b721-af5caf74d7a8"),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
        };

        // Act
        // Assert
        await Assert.ThrowsAsync<AuthorizationException>(() 
            => updateUserCommandHandler.Handle(updateUserCommand, CancellationToken.None));
    }

    [Fact]
    public async Task GivenExistingEmail_WhenUpdateUser_ShouldThrowError()
    {
        // Arrange
        var testEmail = DataUtilityService.GetRandomEmail();
        var user = new List<Users>
        {
            new()
            {
                EmailAddress = testEmail,
                UserAlias = DataUtilityService.GetRandomString(),
                IsActivated = true,
                LastLogged = null,
                CryptedPassword = DataUtilityService.GetRandomString()
            },
            new()
            {
                EmailAddress = testEmail,
                UserAlias = DataUtilityService.GetRandomString(),
                IsActivated = true,
                LastLogged = null,
                CryptedPassword = DataUtilityService.GetRandomString()
            },
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(user);
        await databaseContext.SaveChangesAsync();

        var updateUserCommand = new UpdateUserCommand
        {
            Id = user[0].Id,
            EmailAddress = testEmail,
            UserAlias = DataUtilityService.GetRandomString(),
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            IsActivated = true,
        };

        var mockedDateTime = new Mock<IDateTimeService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();
        var updateUserCommandHandler = new UpdateUserCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object, 
            mockedUserService.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() 
            => updateUserCommandHandler.Handle(updateUserCommand, CancellationToken.None));
    }
}