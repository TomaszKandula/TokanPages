using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Queries.Users;
using TokanPages.Backend.Core.Services.DataProviderService;

namespace TokanPages.Backend.Tests.Handlers.Users
{
    public class GetUserQueryHandlerTest : TestBase
    {
        private readonly DataProviderService FDataProviderService;

        public GetUserQueryHandlerTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public async Task GivenCorrectId_WhenGetUser_ShouldReturnEntity() 
        {
            // Arrange
            var LTestDate = DateTime.Now;
            var LUsers = new TokanPages.Backend.Domain.Entities.Users 
            { 
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                IsActivated = true,
                Registered = LTestDate,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = FDataProviderService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LGetUserQuery = new GetUserQuery { Id = LUsers.Id };
            var LGetUserQueryHandler = new GetUserQueryHandler(LDatabaseContext);

            // Act
            var LResult = await LGetUserQueryHandler.Handle(LGetUserQuery, CancellationToken.None);

            // Assert
            LResult.Should().NotBeNull();
            LResult.Email.Should().Be(LUsers.EmailAddress);
            LResult.AliasName.Should().Be(LUsers.UserAlias);
            LResult.FirstName.Should().Be(LUsers.FirstName);
            LResult.LastName.Should().Be(LUsers.LastName);
            LResult.IsActivated.Should().BeTrue();
            LResult.Registered.Should().Be(LTestDate);
            LResult.LastUpdated.Should().BeNull();
            LResult.LastLogged.Should().BeNull();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenGetUser_ShouldThrowError()
        {
            // Arrange
            var LDatabaseContext = GetTestDatabaseContext();
            var LGetUserQuery = new GetUserQuery { Id = Guid.Parse("8f4cef66-6f37-49bb-bbe7-db6c54336d76") };
            var LGetUserQueryHandler = new GetUserQueryHandler(LDatabaseContext);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LGetUserQueryHandler.Handle(LGetUserQuery, CancellationToken.None));
        }
    }
}
