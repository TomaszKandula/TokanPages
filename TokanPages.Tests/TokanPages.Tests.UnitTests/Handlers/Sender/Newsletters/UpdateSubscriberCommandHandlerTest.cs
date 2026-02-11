using FluentAssertions;
using MediatR;
using Moq;
using TokanPages.Backend.Application.Sender.Newsletters.Commands;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Domain.Entities;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Utility.Abstractions;
using TokanPages.Persistence.DataAccess.Repositories.Sender;
using TokanPages.Persistence.DataAccess.Repositories.Sender.Models;
using TokanPages.Services.UserService.Abstractions;
using TokanPages.Services.UserService.Models;
using Xunit;

namespace TokanPages.Tests.UnitTests.Handlers.Sender.Newsletters;

public class UpdateSubscriberCommandHandlerTest : TestBase
{
    [Fact]
    public async Task GivenNewsletterId_WhenUpdateNewsletter_ShouldSucceed()
    {
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        // Arrange
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockSenderRepository = new Mock<ISenderRepository>();

        var user = new GetUserOutput { UserId = Guid.NewGuid() };
        mockedUserService
            .Setup(service => service.GetUser())
            .ReturnsAsync(user);

        var newsletter = new Newsletter
        {
            Id = Guid.NewGuid(),
            Email = string.Empty,
            IsActivated = false,
            Count = 0,
            CreatedBy = Guid.Empty,
            CreatedAt = default
        };
        mockSenderRepository
            .Setup(repository => repository.GetNewsletter(It.IsAny<Guid>()))
            .ReturnsAsync(newsletter);

        mockSenderRepository
            .Setup(repository => repository.GetNewsletter(It.IsAny<string>()))
            .ReturnsAsync((Newsletter?)null);

        mockSenderRepository
            .Setup(repository => repository.UpdateNewsletter(It.IsAny<UpdateNewsletterDto>()))
            .Returns(Task.CompletedTask);

        var command = new UpdateNewsletterCommand
        {
            Id = Guid.NewGuid(),
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 10
        };

        var handler = new UpdateNewsletterCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object,
            mockSenderRepository.Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().Be(Unit.Value);
    }

    [Fact]
    public async Task GivenInvalidNewsletterId_WhenUpdateNewsletter_ShouldFail()
    {
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        // Arrange
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockSenderRepository = new Mock<ISenderRepository>();

        var user = new GetUserOutput { UserId = Guid.NewGuid() };
        mockedUserService
            .Setup(service => service.GetUser())
            .ReturnsAsync(user);

        mockSenderRepository
            .Setup(repository => repository.GetNewsletter(It.IsAny<Guid>()))
            .ReturnsAsync((Newsletter?)null);

        var command = new UpdateNewsletterCommand
        {
            Id = Guid.NewGuid(),
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 10
        };

        var handler = new UpdateNewsletterCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object,
            mockSenderRepository.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.SUBSCRIBER_DOES_NOT_EXISTS));
    }

    [Fact]
    public async Task GivenExistingEmail_WhenUpdateNewsletter_ShouldFail()
    {
        var databaseContext = GetTestDatabaseContext();//TODO: to be removed

        // Arrange
        var mockedUserService = new Mock<IUserService>();
        var mockedLogger = new Mock<ILoggerService>();
        var mockSenderRepository = new Mock<ISenderRepository>();

        var user = new GetUserOutput { UserId = Guid.NewGuid() };
        mockedUserService
            .Setup(service => service.GetUser())
            .ReturnsAsync(user);

        var newsletter = new Newsletter
        {
            Id = Guid.NewGuid(),
            Email = string.Empty,
            IsActivated = false,
            Count = 0,
            CreatedBy = Guid.Empty,
            CreatedAt = default
        };
        mockSenderRepository
            .Setup(repository => repository.GetNewsletter(It.IsAny<Guid>()))
            .ReturnsAsync(newsletter);

        mockSenderRepository
            .Setup(repository => repository.GetNewsletter(It.IsAny<string>()))
            .ReturnsAsync(newsletter);

        var command = new UpdateNewsletterCommand
        {
            Id = Guid.NewGuid(),
            Email = DataUtilityService.GetRandomEmail(),
            IsActivated = true,
            Count = 10
        };

        var handler = new UpdateNewsletterCommandHandler(
            databaseContext, 
            mockedLogger.Object, 
            mockedUserService.Object,
            mockSenderRepository.Object);

        // Act
        // Assert
        var result = await Assert.ThrowsAsync<BusinessException>(() => handler.Handle(command, CancellationToken.None));
        result.ErrorCode.Should().Be(nameof(ErrorCodes.EMAIL_ADDRESS_ALREADY_EXISTS));
    }
}