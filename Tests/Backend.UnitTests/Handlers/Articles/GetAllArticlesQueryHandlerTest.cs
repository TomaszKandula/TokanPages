using Xunit;
using FluentAssertions;
using Moq;
using MockQueryable.Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.UnitTests.FakeDatabase;
using TokanPages.Backend.Database;
using TokanPages.Backend.Cqrs.Handlers.Queries.Articles;

namespace Backend.UnitTests.Handlers.Articles
{

    public class GetAllArticlesQueryHandlerTest
    {

        private readonly Mock<DatabaseContext> FDatabaseContext;

        public GetAllArticlesQueryHandlerTest() 
        {
            FDatabaseContext = new Mock<TokanPages.Backend.Database.DatabaseContext>();
            var LArticlesDbSet = DummyLoad.GetArticles().AsQueryable().BuildMockDbSet();
            FDatabaseContext.Setup(AMainDbContext => AMainDbContext.Articles).Returns(LArticlesDbSet.Object);
        }

        [Fact]
        public async Task GetAllArticles_ShouldReturnCollection() 
        {

            // Arrange
            var LGetAllArticlesQuery = new GetAllArticlesQuery { };
            var LGetAllArticlesQueryHandler = new GetAllArticlesQueryHandler(FDatabaseContext.Object);

            // Act
            var LResults = await LGetAllArticlesQueryHandler.Handle(LGetAllArticlesQuery, CancellationToken.None);

            // Assert
            LResults.Should().NotBeNull();
            LResults.Should().HaveCount(2);

        }

    }

}
