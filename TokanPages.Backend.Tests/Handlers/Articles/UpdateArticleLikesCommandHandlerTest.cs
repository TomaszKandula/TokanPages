namespace TokanPages.Backend.Tests.Handlers.Articles
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Cqrs.Handlers.Commands.Articles;
    using Cqrs.Services.UserServiceProvider;
    using FluentAssertions;
    using Xunit;
    using Moq;

    public class UpdateArticleLikesCommandHandlerTest : TestBase
    {
        private const string IP_ADDRESS = "255.255.255.255";
        
        [Theory]
        [InlineData(10, 10)]
        [InlineData(50, 25)]
        [InlineData(70, 25)]
        public async Task GivenNewLikesAddedAsAnonymousUser_WhenUpdateArticleLikes_ShouldAddLikes(int ALikes, int AExpectedLikes)
        {
            // Arrange
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                Registered = DataUtilityService.GetRandomDateTime(),
                LastLogged = null,
                LastUpdated = null,
                CryptedPassword = DataUtilityService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = true,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };

            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserServiceProvider>(null, null);
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS);

            var LUpdateArticleLikesCommandHandler = new UpdateArticleLikesCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object);

            var LUpdateArticleLikesCommand = new UpdateArticleLikesCommand
            {
                Id = LArticles.Id,
                AddToLikes = ALikes,
            };

            // Act
            await LUpdateArticleLikesCommandHandler.Handle(LUpdateArticleLikesCommand, CancellationToken.None);

            // Assert
            var LLikesEntity = LDatabaseContext.ArticleLikes
                .Where(AArticleLikes => AArticleLikes.ArticleId == LArticles.Id)
                .ToList();

            var LArticlesEntity = await LDatabaseContext.Articles
                .FindAsync(LUpdateArticleLikesCommand.Id);

            LArticlesEntity.Should().NotBeNull();

            LLikesEntity.Should().HaveCount(1);
            LLikesEntity[0].IpAddress.Should().Be(IP_ADDRESS);
            LLikesEntity[0].UserId.Should().BeNull();
            LLikesEntity[0].LikeCount.Should().Be(AExpectedLikes);
        }

        [Theory]
        [InlineData(10, 10, 20)]
        [InlineData(50, 10, 25)]
        [InlineData(70, 10, 25)]
        public async Task GivenExistingLikesUpdatedAsAnonymousUser_WhenUpdateArticleLikes_ShouldModifyLikes(int AExistingLikes, int ANewLikes, int AExpectedLikes)
        {
            // Arrange
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                Registered = DataUtilityService.GetRandomDateTime(),
                LastLogged = null,
                LastUpdated = null,
                CryptedPassword = DataUtilityService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = true,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };
            
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LLikes = new Domain.Entities.ArticleLikes 
            { 
                ArticleId = LArticles.Id,
                UserId = null,
                IpAddress = IP_ADDRESS,
                LikeCount = AExistingLikes
            };

            await LDatabaseContext.ArticleLikes.AddAsync(LLikes);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserServiceProvider>(null, null);
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS);

            var LUpdateArticleLikesCommandHandler = new UpdateArticleLikesCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object);

            var LUpdateArticleLikesCommand = new UpdateArticleLikesCommand
            {
                Id = LArticles.Id,
                AddToLikes = ANewLikes,
            };

            // Act
            await LUpdateArticleLikesCommandHandler.Handle(LUpdateArticleLikesCommand, CancellationToken.None);

            // Assert
            var LLikesEntity = LDatabaseContext.ArticleLikes
                .Where(AArticleLikes => AArticleLikes.ArticleId == LArticles.Id)
                .ToList();
            
            var LArticlesEntity = await LDatabaseContext.Articles
                .FindAsync(LUpdateArticleLikesCommand.Id);

            LArticlesEntity.Should().NotBeNull();
            LLikesEntity.Should().HaveCount(1);
            LLikesEntity[0].IpAddress.Should().Be(IP_ADDRESS);
            LLikesEntity[0].UserId.Should().BeNull();
            LLikesEntity[0].LikeCount.Should().Be(AExpectedLikes);
        }

        [Theory]
        [InlineData(10, 10)]
        [InlineData(50, 50)]
        [InlineData(70, 50)]
        public async Task GivenNewLikesAddedAsLoggedUser_WhenUpdateArticleLikes_ShouldAddLikes(int ALikes, int AExpectedLikes)
        {
            // Arrange
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                Registered = DataUtilityService.GetRandomDateTime(),
                LastLogged = null,
                LastUpdated = null,
                CryptedPassword = DataUtilityService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = true,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };

            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserServiceProvider>(null, null);
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetUserId())
                .ReturnsAsync(LUsers.Id);

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS);

            var LUpdateArticleLikesCommandHandler = new UpdateArticleLikesCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object);

            var LUpdateArticleLikesCommand = new UpdateArticleLikesCommand
            {
                Id = LArticles.Id,
                AddToLikes = ALikes,
            };

            // Act
            await LUpdateArticleLikesCommandHandler.Handle(LUpdateArticleLikesCommand, CancellationToken.None);

            // Assert
            var LLikesEntity = LDatabaseContext.ArticleLikes
                .Where(AArticleLikes => AArticleLikes.ArticleId == LArticles.Id)
                .ToList();

            var LArticlesEntity = await LDatabaseContext.Articles
                .FindAsync(LUpdateArticleLikesCommand.Id);

            LArticlesEntity.Should().NotBeNull();
            LLikesEntity.Should().HaveCount(1);
            LLikesEntity[0].UserId.Should().NotBeNull();
            LLikesEntity[0].LikeCount.Should().Be(AExpectedLikes);
        }

        [Theory]
        [InlineData(10, 10, 20)]
        [InlineData(50, 10, 50)]
        [InlineData(70, 10, 50)]
        public async Task GivenExistingLikesUpdatedAsLoggedUser_WhenUpdateArticleLikes_ShouldModifyLikes(int AExistingLikes, int ANewLikes, int AExpectedLikes)
        {
            // Arrange
            var LUsers = new TokanPages.Backend.Domain.Entities.Users
            {
                FirstName = DataUtilityService.GetRandomString(),
                LastName = DataUtilityService.GetRandomString(),
                IsActivated = true,
                EmailAddress = DataUtilityService.GetRandomEmail(),
                UserAlias = DataUtilityService.GetRandomString(),
                Registered = DataUtilityService.GetRandomDateTime(),
                LastLogged = null,
                LastUpdated = null,
                CryptedPassword = DataUtilityService.GetRandomString()
            };

            var LDatabaseContext = GetTestDatabaseContext();
            await LDatabaseContext.Users.AddAsync(LUsers);
            await LDatabaseContext.SaveChangesAsync();

            var LArticles = new TokanPages.Backend.Domain.Entities.Articles
            {
                Title = DataUtilityService.GetRandomString(),
                Description = DataUtilityService.GetRandomString(),
                IsPublished = true,
                ReadCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = null,
                UserId = LUsers.Id
            };
            
            await LDatabaseContext.Articles.AddAsync(LArticles);
            await LDatabaseContext.SaveChangesAsync();

            var LLikes = new Domain.Entities.ArticleLikes 
            { 
                ArticleId = LArticles.Id,
                UserId = LUsers.Id,
                IpAddress = IP_ADDRESS,
                LikeCount = AExistingLikes
            };

            await LDatabaseContext.ArticleLikes.AddAsync(LLikes);
            await LDatabaseContext.SaveChangesAsync();

            var LMockedUserProvider = new Mock<UserServiceProvider>(null, null);
            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetUserId())
                .ReturnsAsync(LUsers.Id);

            LMockedUserProvider
                .Setup(AMockedUserProvider => AMockedUserProvider.GetRequestIpAddress())
                .Returns(IP_ADDRESS);

            var LUpdateArticleLikesCommandHandler = new UpdateArticleLikesCommandHandler(
                LDatabaseContext, 
                LMockedUserProvider.Object);

            var LUpdateArticleLikesCommand = new UpdateArticleLikesCommand
            {
                Id = LArticles.Id,
                AddToLikes = ANewLikes,
            };

            // Act
            await LUpdateArticleLikesCommandHandler.Handle(LUpdateArticleLikesCommand, CancellationToken.None);

            // Assert
            var LLikesEntity = LDatabaseContext.ArticleLikes
                .Where(AArticleLikes => AArticleLikes.ArticleId == LArticles.Id)
                .ToList();
            
            var LArticlesEntity = await LDatabaseContext.Articles
                .FindAsync(LUpdateArticleLikesCommand.Id);

            LArticlesEntity.Should().NotBeNull();
            LLikesEntity.Should().HaveCount(1);
            LLikesEntity[0].UserId.Should().NotBeNull();
            LLikesEntity[0].LikeCount.Should().Be(AExpectedLikes);
        }
    }
}