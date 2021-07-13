namespace TokanPages.Backend.Tests.Handlers.Articles
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core.Exceptions;
    using Cqrs.Handlers.Commands.Articles;
    using Shared.Services.DataUtilityService;
    using FluentAssertions;
    using Xunit;

    public class RemoveArticleCommandHandlerTest : TestBase
    {
        private readonly DataUtilityService FDataUtilityService;

        public RemoveArticleCommandHandlerTest() => FDataUtilityService = new DataUtilityService();

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
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                IsActivated = true,
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                UserAlias = FDataUtilityService.GetRandomString(),
                Registered = FDataUtilityService.GetRandomDateTime(),
                LastLogged = null,
                LastUpdated = null,
                CryptedPassword = FDataUtilityService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                Title = FDataUtilityService.GetRandomString(),
                Description = FDataUtilityService.GetRandomString(),
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
                FirstName = FDataUtilityService.GetRandomString(),
                LastName = FDataUtilityService.GetRandomString(),
                IsActivated = true,
                EmailAddress = FDataUtilityService.GetRandomEmail(),
                UserAlias = FDataUtilityService.GetRandomString(),
                Registered = FDataUtilityService.GetRandomDateTime(),
                LastLogged = null,
                LastUpdated = null,
                CryptedPassword = FDataUtilityService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                Title = FDataUtilityService.GetRandomString(),
                Description = FDataUtilityService.GetRandomString(),
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