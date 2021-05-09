﻿using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Core.Generators;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

namespace TokanPages.UnitTests.Handlers.Articles
{
    public class RemoveArticleCommandHandlerTest : TestBase
    {
        [Fact]
        public async Task GivenCorrectId_WhenRemoveArticle_ShouldRemoveEntity() 
        {
            // Arrange
            var LRemoveArticleCommand = new RemoveArticleCommand
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")
            };

            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                IsActivated = true,
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(),
                Registered = DateTimeProvider.GetRandom(),
                LastLogged = null,
                LastUpdated = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };
            
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LRemoveArticleCommandHandler = new RemoveArticleCommandHandler(LDatabaseContext);

            // Act 
            await LRemoveArticleCommandHandler.Handle(LRemoveArticleCommand, CancellationToken.None);

            // Assert
            var LAssertDbContext = GetTestDatabaseContext();
            var LArticlesEntity = await LAssertDbContext.Articles.FindAsync(LRemoveArticleCommand.Id);
            LArticlesEntity.Should().BeNull();
        }

        [Fact]
        public async Task GivenIncorrectId_WhenRemoveArticle_ShouldThrowError()
        {
            // Arrange
            var LRemoveArticleCommand = new RemoveArticleCommand
            {
                Id = Guid.Parse("84e85026-aca9-4709-b9b3-86f2d1300575")
            };

            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = StringProvider.GetRandomString(),
                LastName = StringProvider.GetRandomString(),
                IsActivated = true,
                EmailAddress = StringProvider.GetRandomEmail(),
                UserAlias = StringProvider.GetRandomString(),
                Registered = DateTimeProvider.GetRandom(),
                LastLogged = null,
                LastUpdated = null
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };
            
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LRemoveArticleCommandHandler = new RemoveArticleCommandHandler(LDatabaseContext);

            // Act
            // Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LRemoveArticleCommandHandler.Handle(LRemoveArticleCommand, CancellationToken.None));
        }
    }
}
