﻿namespace TokanPages.Tests.UnitTests.Handlers.Articles;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.Core.Exceptions;
using Backend.Domain.Entities;
using TokanPages.Services.UserService;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Handlers.Commands.Articles;

public class RemoveArticleCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenCorrectId_WhenRemoveArticle_ShouldRemoveEntity() 
    {
        // Arrange
        var removeArticleCommand = new RemoveArticleCommand
        {
            Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")
        };

        var users = new Users
        {
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var articles = new Articles
        {
            Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = false,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = users.Id
        };

        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();

        var removeArticleCommandHandler = new RemoveArticleCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object);

        // Act 
        await removeArticleCommandHandler.Handle(removeArticleCommand, CancellationToken.None);

        // Assert
        var assertDbContext = GetTestDatabaseContext();
        var articlesEntity = await assertDbContext.Articles.FindAsync(removeArticleCommand.Id);
        articlesEntity.Should().BeNull();
    }

    [Fact]
    public async Task GivenIncorrectId_WhenRemoveArticle_ShouldThrowError()
    {
        // Arrange
        var removeArticleCommand = new RemoveArticleCommand
        {
            Id = Guid.Parse("84e85026-aca9-4709-b9b3-86f2d1300575")
        };

        var users = new Users
        {
            IsActivated = true,
            EmailAddress = DataUtilityService.GetRandomEmail(),
            UserAlias = DataUtilityService.GetRandomString(),
            CryptedPassword = DataUtilityService.GetRandomString()
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddAsync(users);
        await databaseContext.SaveChangesAsync();

        var articles = new Articles
        {
            Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
            Title = DataUtilityService.GetRandomString(),
            Description = DataUtilityService.GetRandomString(),
            IsPublished = false,
            ReadCount = 0,
            CreatedAt = DateTime.Now,
            UpdatedAt = null,
            UserId = users.Id
        };

        await databaseContext.Articles.AddAsync(articles);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedUserService = new Mock<IUserService>();

        var removeArticleCommandHandler = new RemoveArticleCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() 
            => removeArticleCommandHandler.Handle(removeArticleCommand, CancellationToken.None));
    }
}