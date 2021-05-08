using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Tests.DataProviders;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Commands.Users;

namespace TokanPages.Tests.UnitTests.Handlers.Users
{
    public class RemoveUserCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenCorrectId_WhenRemoveUser_ShouldRemoveEntity() 
        {
            // Arrange
            var LUsers = new TokanPages.Backend.Domain.Entities.Users 
            { 
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(),
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LRemoveUserCommand = new RemoveUserCommand { Id = LUsers.Id };
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
            var LDatabaseContext = GetTestDatabaseContext();
            var LRemoveUserCommand = new RemoveUserCommand { Id = Guid.Parse("275c1659-ebe2-44ca-b912-b93b1861a9fb") };
            var LRemoveUserCommandHandler = new RemoveUserCommandHandler(LDatabaseContext);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LRemoveUserCommandHandler.Handle(LRemoveUserCommand, CancellationToken.None));
        }
    }
}
