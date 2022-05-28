﻿namespace TokanPages.Tests.UnitTests.Handlers.Users;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Domain.Entities;
using Backend.Core.Exceptions;
using Backend.Cqrs.Handlers.Queries.Users;
using Backend.Core.Utilities.LoggerService;

public class GetUserQueryHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectId_WhenGetUser_ShouldReturnEntity() 
    {
        // Arrange
        var users = new Users 
        { 
            Id = Guid.NewGuid(),
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            IsActivated = true,
            LastLogged = null,
            CryptedPassword = DataUtilityService.GetRandomString()
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
        var getUserQuery = new GetUserQuery { Id = users.Id };
        var getUserQueryHandler = new GetUserQueryHandler(databaseContext, mockedLogger.Object);

        // Act
        var result = await getUserQueryHandler.Handle(getUserQuery, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be(users.EmailAddress);
        result.AliasName.Should().Be(users.UserAlias);
        result.IsActivated.Should().BeTrue();
        //result.Registered.Should().Be(userInfo.CreatedAt);//TODO: use [Users].[CreatedAt]
        result.LastUpdated.Should().BeNull();
        result.LastLogged.Should().BeNull();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenGetUser_ShouldThrowError()
    {
        // Arrange
        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();

        var getUserQuery = new GetUserQuery { Id = Guid.Parse("8f4cef66-6f37-49bb-bbe7-db6c54336d76") };
        var getUserQueryHandler = new GetUserQueryHandler(databaseContext, mockedLogger.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() 
            => getUserQueryHandler.Handle(getUserQuery, CancellationToken.None));
    }
}