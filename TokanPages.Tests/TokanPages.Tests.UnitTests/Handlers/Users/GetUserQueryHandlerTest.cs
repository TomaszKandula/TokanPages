﻿using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Users.Queries;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.LoggerService;
using TokanPages.Backend.Domain.Entities.User;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class GetUserQueryHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectId_WhenGetUser_ShouldReturnEntity() 
    {
        // Arrange
        var users = new Backend.Domain.Entities.User.Users 
        { 
            Id = Guid.NewGuid(),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            CryptedPassword = DataUtilityService.GetRandomString(),
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.Empty
        };

        var userInfo = new UserInfo
        {
            UserId = users.Id,
            FirstName = DataUtilityService.GetRandomString(),
            LastName = DataUtilityService.GetRandomString(),
            UserAboutText = DataUtilityService.GetRandomString(),
            UserImageName = null,
            UserVideoName = null,
            CreatedBy = Guid.Empty,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            ModifiedBy = null,
            ModifiedAt = null
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.UserInfo.AddAsync(userInfo);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var query = new GetUserQuery { Id = users.Id };
        var handler = new GetUserQueryHandler(databaseContext, mockedLogger.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(users.EmailAddress);
        result.AliasName.Should().Be(users.UserAlias);
        result.IsActivated.Should().BeTrue();
        result.Registered.Should().Be(users.CreatedAt);
        result.LastUpdated.Should().BeNull();
        result.LastLogged.Should().BeNull();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenGetUser_ShouldThrowError()
    {
        // Arrange
        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();

        var query = new GetUserQuery { Id = Guid.NewGuid() };
        var handler = new GetUserQueryHandler(databaseContext, mockedLogger.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(query, CancellationToken.None));
    }
}