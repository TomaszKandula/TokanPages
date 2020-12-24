using Xunit;
using Moq;
using MockQueryable.Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

namespace Backend.UnitTests.Handlers.Articles
{

    public class RemoveArticleCommandHandlerTest
    {

        [Fact]
        public async Task RemoveArticle_WhenIdIsCorrect_ShouldFinishSuccessfull() 
        {

            // Arrange
            var LRemoveArticleCommand = new RemoveArticleCommand
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")
            };

            var LDatabaseContext = new Mock<DatabaseContext>();
            var LDummyLoad = new List<TokanPages.Backend.Domain.Entities.Articles>
            {
                new TokanPages.Backend.Domain.Entities.Articles
                {
                    Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9"),
                    Title = "Why C# is great?",
                    Description = "More on C#",
                    IsPublished = false,
                    Likes = 0,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                }
            };

            var LArticlesDbSet = LDummyLoad.AsQueryable().BuildMockDbSet();
            LDatabaseContext.Setup(AMainDbContext => AMainDbContext.Articles).Returns(LArticlesDbSet.Object);

            var LRemoveArticleCommandHandler = new RemoveArticleCommandHandler(LDatabaseContext.Object);

            // Act 
            await LRemoveArticleCommandHandler.Handle(LRemoveArticleCommand, CancellationToken.None);

            // Assert
            LDatabaseContext.Verify(DbContext => DbContext.SaveChangesAsync(CancellationToken.None), Times.Once);

        }

        [Fact]
        public async Task RemoveArticle_WhenIdIsIncorrect_ShouldThrowError()
        {

            // Arrange
            var LRemoveArticleCommand = new RemoveArticleCommand
            {
                Id = Guid.Parse("84e85026-aca9-4709-b9b3-86f2d1300575")
            };

            var LDatabaseContext = new Mock<DatabaseContext>();
            var LDummyLoad = new List<TokanPages.Backend.Domain.Entities.Articles>
            {
                new TokanPages.Backend.Domain.Entities.Articles
                {
                    Id = Guid.Parse("fbc54b0f-bbec-406f-b8a9-0a1c5ca1e841"),
                    Title = "NET Core 5 is coming",
                    Description = "What's new?",
                    IsPublished = false,
                    Likes = 0,
                    ReadCount = 0,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                }
            };

            var LArticlesDbSet = LDummyLoad.AsQueryable().BuildMockDbSet();
            LDatabaseContext.Setup(AMainDbContext => AMainDbContext.Articles).Returns(LArticlesDbSet.Object);

            var LRemoveArticleCommandHandler = new RemoveArticleCommandHandler(LDatabaseContext.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LRemoveArticleCommandHandler.Handle(LRemoveArticleCommand, CancellationToken.None));

        }

    }

}
