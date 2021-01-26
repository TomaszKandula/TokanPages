using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.TestData;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Commands.Users;

namespace Backend.UnitTests.Handlers.Users
{   
    public class AddUserCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task AddUser_WhenFieldsAreProvided_ShouldAddEntity() 
        {
            // Arrange
            var LAddUserCommand = new AddUserCommand 
            {
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LAddUserCommandHandler = new AddUserCommandHandler(LDatabaseContext);

            // Act
            await LAddUserCommandHandler.Handle(LAddUserCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LResult = LAssertDbContext.Users.ToList();

            LResult.Should().NotBeNull();
            LResult.Should().HaveCount(1);
            LResult[0].EmailAddress.Should().Be(LAddUserCommand.EmailAddress);
            LResult[0].UserAlias.Should().Be(LAddUserCommand.UserAlias);
            LResult[0].FirstName.Should().Be(LAddUserCommand.FirstName);
            LResult[0].LastName.Should().Be(LAddUserCommand.LastName);
            LResult[0].IsActivated.Should().BeTrue();
            LResult[0].LastLogged.Should().BeNull();
            LResult[0].LastUpdated.Should().BeNull();
        }

        [Fact]
        public async Task AddUser_WhenEmailExists_ShouldThrowError()
        {
            // Arrange
            var LTestEmail = DataProvider.GetRandomEmail();
            var LAddUserCommand = new AddUserCommand
            {
                EmailAddress = LTestEmail,
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            { 
                Id = Guid.Parse("c11c0bf3-e585-4f1d-8b1a-ff6049fd667c"),
                EmailAddress = LTestEmail,
                IsActivated = false,
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null
            };
            LDatabaseContext.Users.Add(LUsers);
            LDatabaseContext.SaveChanges();

            var LAddUserCommandHandler = new AddUserCommandHandler(LDatabaseContext);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LAddUserCommandHandler.Handle(LAddUserCommand, CancellationToken.None));
        }
    }
}
