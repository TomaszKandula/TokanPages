using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.TestData;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Commands.Users;

namespace Backend.UnitTests.Handlers.Users
{

    public class RemoveUserCommandHandlerTest : TestBase
    {

        [Fact]
        public async Task RemoveUser_WhenIdIsCorrect_ShouldRemoveEntity() 
        {

            // Arrange
            var LRemoveUserCommand = new RemoveUserCommand 
            { 
                Id = Guid.Parse("abf7c26c-e05d-4b6b-8f1c-0e2551026cf4")
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LUsers = new TokanPages.Backend.Domain.Entities.Users 
            { 
                Id = Guid.Parse("abf7c26c-e05d-4b6b-8f1c-0e2551026cf4"),
                EmailAddress = DataProvider.GetRandomEmail(),
                UserAlias = DataProvider.GetRandomString(),
                FirstName = DataProvider.GetRandomString(),
                LastName = DataProvider.GetRandomString(),
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null
            };
            LDatabaseContext.Users.Add(LUsers);
            LDatabaseContext.SaveChanges();

            var LRemoveUserCommandHandler = new RemoveUserCommandHandler(LDatabaseContext);

            // Act
            await LRemoveUserCommandHandler.Handle(LRemoveUserCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LResults = LAssertDbContext.Users.Find(LRemoveUserCommand.Id);
            LResults.Should().BeNull();

        }

        [Fact]
        public async Task RemoveUser_WhenIdIsIncorrect_ShouldThrowError()
        {

            // Arrange
            var LRemoveUserCommand = new RemoveUserCommand
            {
                Id = Guid.Parse("275c1659-ebe2-44ca-b912-b93b1861a9fb")
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LRemoveUserCommandHandler = new RemoveUserCommandHandler(LDatabaseContext);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LRemoveUserCommandHandler.Handle(LRemoveUserCommand, CancellationToken.None));

        }

    }

}
