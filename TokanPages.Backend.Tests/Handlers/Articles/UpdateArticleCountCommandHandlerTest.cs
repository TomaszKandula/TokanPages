using Xunit;
using FluentAssertions;
using System;
using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Shared.Resources;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;
using TokanPages.Backend.Shared.Services.DataProviderService;

namespace TokanPages.Backend.Tests.Handlers.Articles
{
    public class UpdateArticleCountCommandHandlerTest : TestBase
    {
        private readonly DataProviderService FDataProviderService;

        public UpdateArticleCountCommandHandlerTest() => FDataProviderService = new DataProviderService();

        [Fact]
        public async Task GivenExistingArticle_WhenUpdateArticleCount_ShouldReturnSuccessful()
        {
            // Arrange
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                IsActivated = true,
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                Registered = FDataProviderService.GetRandomDateTime(),
                LastLogged = null,
                LastUpdated = null,
                CryptedPassword = FDataProviderService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };

            var LExpectedReadCount = LArticles.ReadCount + 1;
            
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LUpdateArticleCountCommandHandler = new UpdateArticleCountCommandHandler(LDatabaseContext);
            var LUpdateArticleCommand = new UpdateArticleCountCommand { Id = LArticles.Id };

            // Act
            await LUpdateArticleCountCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None);

            // Assert
            var LArticlesEntity = await LDatabaseContext.Articles
                .FindAsync(LUpdateArticleCommand.Id);

            LArticlesEntity.Should().NotBeNull();
            LArticlesEntity.ReadCount.Should().Be(LExpectedReadCount);
        }
        
        [Fact]
        public async Task GivenExistingArticleAndIncorrectArticleId_WhenUpdateArticleCount_ShouldThrowError()
        {
            // Arrange
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = FDataProviderService.GetRandomString(),
                LastName = FDataProviderService.GetRandomString(),
                IsActivated = true,
                EmailAddress = FDataProviderService.GetRandomEmail(),
                UserAlias = FDataProviderService.GetRandomString(),
                Registered = FDataProviderService.GetRandomDateTime(),
                LastLogged = null,
                LastUpdated = null,
                CryptedPassword = FDataProviderService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();
            
            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Title = FDataProviderService.GetRandomString(),
                Description = FDataProviderService.GetRandomString(),
                IsPublished = false,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };
            
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LUpdateArticleCountCommandHandler = new UpdateArticleCountCommandHandler(LDatabaseContext);
            var LUpdateArticleCommand = new UpdateArticleCountCommand { Id = Guid.NewGuid() };

            // Act
            // Assert
            var LResult = await Assert.ThrowsAsync<BusinessException>(() 
                => LUpdateArticleCountCommandHandler.Handle(LUpdateArticleCommand, CancellationToken.None));
            LResult.ErrorCode.Should().Be(nameof(ErrorCodes.ARTICLE_DOES_NOT_EXISTS));
        }
        
    }
}