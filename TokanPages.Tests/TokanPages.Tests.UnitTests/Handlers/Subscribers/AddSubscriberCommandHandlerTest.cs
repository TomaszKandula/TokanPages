using FluentAssertions;
using Moq;
using TokanPages.Backend.Application.Subscribers.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Utilities.DateTimeService;
using TokanPages.Backend.Core.Utilities.LoggerService;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Subscribers;

public class AddSubscriberCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenProvidedEmail_WhenAddSubscriber_ShouldAddEntity() 
    {
        // Arrange
        var databaseContext = GetTestDatabaseContext();
        var mockedLogger = new Mock<ILoggerService>();
        var dateTimeService = new DateTimeService();

        var command = new AddSubscriberCommand { Email = DataUtilityService.GetRandomEmail() };
        var handler = new AddSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            dateTimeService);

        // Act
        await handler.Handle(command, CancellationToken.None);

        // Assert
        var entity = databaseContext.Newsletters.ToList();

        entity.Should().HaveCount(1);
        entity[0].Email.Should().Be(command.Email);
        entity[0].Count.Should().Be(0);
        entity[0].IsActivated.Should().BeTrue();
        entity[0].CreatedAt.Should().BeBefore(DateTime.UtcNow);
        entity[0].CreatedBy.Should().Be(Guid.Empty);
    }

    [Fact]
    public async Task GivenExistingEmail_WhenAddSubscriber_ShouldThrowError()
    {
        // Arrange
        var testEmail = DataUtilityService.GetRandomEmail();
        var subscribers = new TokanPages.Backend.Domain.Entities.Newsletters 
        { 
            Email = testEmail,
            IsActivated = true,
            Count = 0,
            CreatedAt = DataUtilityService.GetRandomDateTime(),
            CreatedBy = Guid.Empty
        };

        var databaseContext = GetTestDatabaseContext();
        await databaseContext.Newsletters.AddAsync(subscribers);
        await databaseContext.SaveChangesAsync();

        var mockedLogger = new Mock<ILoggerService>();
        var mockedDateTime = new Mock<IDateTimeService>();

        var command = new AddSubscriberCommand { Email = testEmail };
        var handler = new AddSubscriberCommandHandler(
            databaseContext, 
            mockedLogger.Object,
            mockedDateTime.Object);

        // Act
        // Assert
        await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
    }
}