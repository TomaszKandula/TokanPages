namespace TokanPages.Backend.Tests.Handlers.Subscribers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Exceptions;
    using Cqrs.Handlers.Queries.Subscribers;
    using Shared.Services.DataUtilityService;
    using FluentAssertions;
    using Xunit;

    public class GetSubscriberQueryHandlerTest : TestBase
    {
        private readonly DataUtilityService FDataUtilityService;

        public GetSubscriberQueryHandlerTest() => FDataUtilityService = new DataUtilityService();

        [Fact]
        public async Task GivenCorrectId_WhenGetSubscriber_ShouldReturnEntity() 
        {
            // Arrange
            var LTestDate = DateTime.Now;
            var LSubscribers = new TokanPages.Backend.Domain.Entities.Subscribers
            {
                Email = FDataUtilityService.GetRandomEmail(),
                IsActivated = true,
                Count = 10,
                Registered = LTestDate,
                LastUpdated = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Subscribers.AddAsync(LSubscribers);
            await LDatabaseContext.SaveChangesAsync();

            var LGetSubscriberQuery = new GetSubscriberQuery { Id = LSubscribers.Id };

            // Act
            var LGetSubscriberQueryHandler = new GetSubscriberQueryHandler(LDatabaseContext);
            var LResults = await LGetSubscriberQueryHandler.Handle(LGetSubscriberQuery, CancellationToken.None);

            // Assert
            LResults.Should().NotBeNull();
            LResults.Email.Should().Be(LSubscribers.Email);
            LResults.IsActivated.Should().BeTrue();
            LResults.NewsletterCount.Should().Be(LSubscribers.Count);
            LResults.Registered.Should().Be(LTestDate);
            LResults.LastUpdated.Should().BeNull();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenGetSubscriber_ShouldThrowError()
        {
            // Arrange
            var LSubscribers = new TokanPages.Backend.Domain.Entities.Subscribers
            {
                Email = FDataUtilityService.GetRandomEmail(),
                IsActivated = true,
                Count = 10,
                Registered = DateTime.Now,
                LastUpdated = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Subscribers.AddAsync(LSubscribers);
            await LDatabaseContext.SaveChangesAsync();

            var LGetSubscriberQueryHandler = new GetSubscriberQueryHandler(LDatabaseContext);
            var LGetSubscriberQuery = new GetSubscriberQuery
            {
                Id = Guid.Parse("b6bb37e0-bad3-419c-b61f-45318cb7b68c")
            };

            // Act
            // Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LGetSubscriberQueryHandler.Handle(LGetSubscriberQuery, CancellationToken.None));
        }
    }
}