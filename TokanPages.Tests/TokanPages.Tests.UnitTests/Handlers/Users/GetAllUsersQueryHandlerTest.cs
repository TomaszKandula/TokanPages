using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Users.Queries;
using TokanPages.Backend.Core.Utilities.LoggerService;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Users;

public class GetAllUsersQueryHandlerTest : TestBase
{
    [Fact]
    public async Task WhenGetAllArticles_ShouldReturnCollection()
    {
        // Arrange
        var users = new List<Backend.Domain.Entities.Users>
        {
            new()
            {
                Id = Guid.NewGuid(),
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                IsActivated = true,
                CryptedPassword = DataUtilityService.GetRandomString()
            },
            new()
            {
                Id = Guid.NewGuid(),
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                IsActivated = true,
                CryptedPassword = DataUtilityService.GetRandomString()
            }
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Users.AddRangeAsync(users);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();

        var query = new GetAllUsersQuery();
        var handler = new GetAllUsersQueryHandler(databaseContext, mockedLogger.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.ToList().Should().NotBeNull();
        result.ToList().Should().HaveCount(2);
    }
}