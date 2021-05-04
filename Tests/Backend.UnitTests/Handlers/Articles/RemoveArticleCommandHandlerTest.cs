﻿using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using Backend.DataProviders;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

namespace Backend.UnitTests.Handlers.Articles
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

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Articles.AddAsync(new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            });
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

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Articles.AddAsync(new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                Title = StringProvider.GetRandomString(),
                Description = StringProvider.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            });
            await LDatabaseContext.SaveChangesAsync();

            var LRemoveArticleCommandHandler = new RemoveArticleCommandHandler(LDatabaseContext);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() 
                => LRemoveArticleCommandHandler.Handle(LRemoveArticleCommand, CancellationToken.None));
        }
    }
}
