namespace TokanPages.Backend.Tests.Handlers.Subscribers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Exceptions;
    using Cqrs.Handlers.Commands.Subscribers;
    using Shared.Services.DataUtilityService;
    using FluentAssertions;
    using Xunit;

    public class RemoveSubscriberCommandHandlerTest : TestBase
    {
        private readonly DataUtilityService FDataUtilityService;

        public RemoveSubscriberCommandHandlerTest() => FDataUtilityService = new DataUtilityService();

        [Fact]
        public async Task GivenCorrectId_WhenRemoveSubscriber_ShouldRemoveEntity() 
        {
            // Arrange
            var LSubscribers = new TokanPages.Backend.Domain.Entities.Subscribers 
            {
                Email = FDataUtilityService.GetRandomEmail(),
                IsActivated = true,
                Count = 50,
                Registered = DateTime.Now,
                LastUpdated = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Subscribers.AddAsync(LSubscribers);
            await LDatabaseContext.SaveChangesAsync();

            var LRemoveSubscriberCommand = new RemoveSubscriberCommand { Id = LSubscribers.Id };
            var LRemoveSubscriberCommandHandler = new RemoveSubscriberCommandHandler(LDatabaseContext);

            // Act
            await LRemoveSubscriberCommandHandler.Handle(LRemoveSubscriberCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LSubscribersEntity = await LAssertDbContext.Subscribers.FindAsync(LRemoveSubscriberCommand.Id);
            LSubscribersEntity.Should().BeNull();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenRemoveSubscriber_ShouldThrowError()
        {
            // Arrange
            var LRemoveSubscriberCommand = new RemoveSubscriberCommand
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LRemoveSubscriberCommandHandler = new RemoveSubscriberCommandHandler(LDatabaseContext);

            // Act
            // Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LRemoveSubscriberCommandHandler.Handle(LRemoveSubscriberCommand, CancellationToken.None));
        }
    }
}