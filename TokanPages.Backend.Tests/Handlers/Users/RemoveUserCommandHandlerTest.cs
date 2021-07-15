namespace TokanPages.Backend.Tests.Handlers.Users
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Exceptions;
    using Cqrs.Handlers.Commands.Users;
    using FluentAssertions;
    using Xunit;

    public class RemoveUserCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenCorrectId_WhenRemoveUser_ShouldRemoveEntity() 
        {
            // Arrange
            var LUsers = new TokanPages.Backend.Domain.Entities.Users 
            { 
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                Registered = DateTime.Now,
                LastUpdated = null,
                LastLogged = null,
                CryptedPassword = DataUtilityService.GetRandomString()
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

            // Act
            // Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LRemoveUserCommandHandler.Handle(LRemoveUserCommand, CancellationToken.None));
        }
    }
}