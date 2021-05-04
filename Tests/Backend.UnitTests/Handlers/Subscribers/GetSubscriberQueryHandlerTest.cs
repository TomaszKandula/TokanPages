using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.TestData;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Queries.Subscribers;

namespace Backend.UnitTests.Handlers.Subscribers
{
    public class GetSubscriberQueryHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenCorrectId_WhenGetSubscriber_ShouldReturnEntity() 
        {
            // Arrange
            var LGetSubscriberQuery = new GetSubscriberQuery
            {
                Id = Guid.Parse("2992bc4c-d7a6-43c4-b3e8-f5f632eb229d")
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LTestDate = DateTime.Now;
            var LSubscribers = new TokanPages.Backend.Domain.Entities.Subscribers
            {
                Id = Guid.Parse("2992bc4c-d7a6-43c4-b3e8-f5f632eb229d"),
                Email = DataProvider.GetRandomEmail(),
                IsActivated = true,
                Count = 10,
                Registered = LTestDate,
                LastUpdated = null
            };
            await LDatabaseContext.Subscribers.AddAsync(LSubscribers);
            await LDatabaseContext.SaveChangesAsync();

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
            var LGetSubscriberQuery = new GetSubscriberQuery
            {
                Id = Guid.Parse("b6bb37e0-bad3-419c-b61f-45318cb7b68c")
            };

            var LDatabaseContext = GetTestDatabaseContext();
            var LSubscribers = new TokanPages.Backend.Domain.Entities.Subscribers
            {
                Id = Guid.Parse("2992bc4c-d7a6-43c4-b3e8-f5f632eb229d"),
                Email = DataProvider.GetRandomEmail(),
                IsActivated = true,
                Count = 10,
                Registered = DateTime.Now,
                LastUpdated = null
            };
            await LDatabaseContext.Subscribers.AddAsync(LSubscribers);
            await LDatabaseContext.SaveChangesAsync();

            var LGetSubscriberQueryHandler = new GetSubscriberQueryHandler(LDatabaseContext);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LGetSubscriberQueryHandler.Handle(LGetSubscriberQuery, CancellationToken.None));
        }
    }
}
