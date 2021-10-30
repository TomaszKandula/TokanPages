namespace TokanPages.Backend.Tests.Handlers.Users
{
    using Moq;
    using Xunit;
    using FluentAssertions;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Utilities.LoggerService;
    using Domain.Entities;
    using Core.Exceptions;
    using Cqrs.Handlers.Queries.Users;

    public class GetUserQueryHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenCorrectId_WhenGetUser_ShouldReturnEntity() 
        {
            // Arrange
            var testDate = DateTime.Now;
            var users = new Users 
            { 
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = testDate,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = DataUtilityService.GetRandomString()
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Users.AddAsync(users);
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
            result.FirstName.Should().Be(users.FirstName);
            result.LastName.Should().Be(users.LastName);
            result.IsActivated.Should().BeTrue();
            result.Registered.Should().Be(testDate);
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
}