using Xunit;
using Moq;
using MockQueryable.Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.UnitTests.FakeDatabase;
using TokanPages.Backend.Database;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Cqrs.Handlers.Commands.Articles;

namespace Backend.UnitTests.Handlers.Articles
{

    public class RemoveArticleCommandHandlerTest
    {

        private readonly Mock<DatabaseContext> FDatabaseContext;

        public RemoveArticleCommandHandlerTest() 
        {
            FDatabaseContext = new Mock<DatabaseContext>();
            var LArticlesDbSet = DummyLoad.GetArticles().AsQueryable().BuildMockDbSet();
            FDatabaseContext.Setup(DatabaseContext => DatabaseContext.Articles).Returns(LArticlesDbSet.Object);
        }

        [Fact]
        public async Task RemoveArticle_WhenIdIsCorrect_ShouldFinishSuccessfull() 
        {

            // Arrange
            var LRemoveArticleCommand = new RemoveArticleCommand
            {
                Id = Guid.Parse("2431eeba-866c-4e45-ad64-c409dd824df9")
            };

            var LRemoveArticleCommandHandler = new RemoveArticleCommandHandler(FDatabaseContext.Object);

            // Act 
            await LRemoveArticleCommandHandler.Handle(LRemoveArticleCommand, CancellationToken.None);

            // Assert
            FDatabaseContext.Verify(DbContext => DbContext.SaveChangesAsync(CancellationToken.None), Times.Once);

        }

        [Fact]
        public async Task RemoveArticle_WhenIdIsIncorrect_ShouldThrowError()
        {

            // Arrange
            var LRemoveArticleCommand = new RemoveArticleCommand
            {
                Id = Guid.Parse("84e85026-aca9-4709-b9b3-86f2d1300575")
            };

            var LRemoveArticleCommandHandler = new RemoveArticleCommandHandler(FDatabaseContext.Object);

            // Act & Assert
            await Assert.ThrowsAsync<BusinessException>(() => LRemoveArticleCommandHandler.Handle(LRemoveArticleCommand, CancellationToken.None));

        }

    }

}
