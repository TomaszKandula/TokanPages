using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Services.DataProviderService;
using TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers;

namespace TokanPages.Backend.Tests.Handlers.Subscribers
{
    public class GetSubscriberQueryHandlerTest : TestBase
    {
        private readonly DataProviderService FDataProviderService;

        public GetSubscriberQueryHandlerTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public async Task GivenCorrectId_WhenGetSubscriber_ShouldReturnEntity() 
        {
            // Arrange
            var LTestDate = DateTime.Now;
            var LSubscribers = new TokanPages.Backend.Domain.Entities.Subscribers
            {
                Email = FDataProviderService.GetRandomEmail(),
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
                Email = FDataProviderService.GetRandomEmail(),
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
