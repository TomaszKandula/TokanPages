namespace TokanPages.Tests.UnitTests.Handlers.Users;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Entities;
using Backend.Core.Exceptions;
using TokanPages.Services.UserService;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Handlers.Commands.Users;

public class RemoveUserCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectId_WhenRemoveUser_ShouldRemoveEntity() 
    {
        // Arrange
        var users = new Users 
        { 
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            LastLogged = null,
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();
        var removeUserCommand = new RemoveUserCommand { Id = users.Id };
        var removeUserCommandHandler = new RemoveUserCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object);

        // Act
        await removeUserCommandHandler.Handle(removeUserCommand, CancellationToken.None);

        // Assert
        var assertDbContext = GetTestDatabaseContext();
        var result = await assertDbContext.Users.FindAsync(removeUserCommand.Id);
        result.Should().BeNull();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenRemoveUser_ShouldThrowError()
    {
        // Arrange
        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();

        var removeUserCommand = new RemoveUserCommand { Id = Guid.Parse("275c1659-ebe2-44ca-b912-b93b1861a9fb") };
        var removeUserCommandHandler = new RemoveUserCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<AuthorizationException>(() 
            => removeUserCommandHandler.Handle(removeUserCommand, CancellationToken.None));
    }
}