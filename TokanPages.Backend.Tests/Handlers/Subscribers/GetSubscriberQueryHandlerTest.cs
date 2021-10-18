﻿namespace TokanPages.Backend.Tests.Handlers.Subscribers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Exceptions;
    using Cqrs.Handlers.Queries.Subscribers;
    using FluentAssertions;
    using Xunit;

    public class GetSubscriberQueryHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenCorrectId_WhenGetSubscriber_ShouldReturnEntity() 
        {
            // Arrange
            var testDate = DateTime.Now;
            var subscribers = new TokanPages.Backend.Domain.Entities.Subscribers
            {
                Email = DataUtilityService.GetRandomEmail(),
                IsActivated = true,
                Count = 10,
                Registered = testDate,
                LastUpdated = null
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Subscribers.AddAsync(subscribers);
            await databaseContext.SaveChangesAsync();

            var getSubscriberQuery = new GetSubscriberQuery { Id = subscribers.Id };

            // Act
            var getSubscriberQueryHandler = new GetSubscriberQueryHandler(databaseContext);
            var result = await getSubscriberQueryHandler.Handle(getSubscriberQuery, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Email.Should().Be(subscribers.Email);
            result.IsActivated.Should().BeTrue();
            result.NewsletterCount.Should().Be(subscribers.Count);
            result.Registered.Should().Be(testDate);
            result.LastUpdated.Should().BeNull();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenGetSubscriber_ShouldThrowError()
        {
            // Arrange
            var subscribers = new TokanPages.Backend.Domain.Entities.Subscribers
            {
                Email = DataUtilityService.GetRandomEmail(),
                IsActivated = true,
                Count = 10,
                Registered = DateTime.Now,
                LastUpdated = null
            };

            var databaseContext = GetTestDatabaseContext();
            await databaseContext.Subscribers.AddAsync(subscribers);
            await databaseContext.SaveChangesAsync();

            var getSubscriberQueryHandler = new GetSubscriberQueryHandler(databaseContext);
            var getSubscriberQuery = new GetSubscriberQuery
            {
                Id = Guid.Parse("b6bb37e0-bad3-419c-b61f-45318cb7b68c")
            };

            // Act
            // Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => getSubscriberQueryHandler.Handle(getSubscriberQuery, CancellationToken.None));
        }
    }
}