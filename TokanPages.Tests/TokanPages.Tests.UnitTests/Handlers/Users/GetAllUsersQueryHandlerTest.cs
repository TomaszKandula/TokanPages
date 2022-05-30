﻿namespace TokanPages.Tests.UnitTests.Handlers.Users;

using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Backend.Domain.Entities;
using Backend.Core.Utilities.LoggerService;
using Backend.Cqrs.Handlers.Queries.Users;

public class GetAllUsersQueryHandlerTest : TestBase
{
    [Fact]
    public async Task WhenGetAllArticles_ShouldReturnCollection()
    {
        // Arrange
        var users = new List<Users>
        {
            new()
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                IsActivated = true,
                CryptedPassword = DataUtilityService.GetRandomString()
            },
            new()
            {
                Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                IsActivated = true,
                CryptedPassword = DataUtilityService.GetRandomString()
            }
        };

        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();

        var getAllUsersQuery = new GetAllUsersQuery();
        var getAllUsersQueryHandler = new GetAllUsersQueryHandler(databaseContext, mockedLogger.Object);

        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.SaveChangesAsync();

        // Act
        var result = (await getAllUsersQueryHandler
                .Handle(getAllUsersQuery, CancellationToken.None))
            .ToList();

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
    }
}