using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataProviders;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Commands.Users;

namespace Backend.UnitTests.Handlers.Users
{
    public class RemoveUserCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenCorrectId_WhenRemoveUser_ShouldRemoveEntity() 
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
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(),
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null
            };
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LRemoveUserCommandHandler = new RemoveUserCommandHandler(LDatabaseContext);

            // Act
            await LRemoveUserCommandHandler.Handle(LRemoveUserCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LResults = await LAssertDbContext.Users.FindAsync(LRemoveUserCommand.Id);
            LResults.Should().BeNull();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenRemoveUser_ShouldThrowError()
        {
            // Arrange
            var LRemoveUserCommand = new RemoveUserCommand
            {
                Id = Guid.Parse("275c1659-ebe2-44ca-b912-b93b1861a9fb")
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LRemoveUserCommandHandler = new RemoveUserCommandHandler(LDatabaseContext);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LRemoveUserCommandHandler.Handle(LRemoveUserCommand, CancellationToken.None));
        }
    }
}
