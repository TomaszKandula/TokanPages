using Xunit;
using FluentAssertions;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.UnitTests.Mock;
using BackEnd.UnitTests.Database;
using TokanPages.BackEnd.Logic.Articles;

namespace BackEnd.UnitTests
{

    public class ArticlesTests
    {

        [Fact]
        public async Task GetAllArticles_Test()
        {

            // Arrange
            var LFakeCosmosService = new FakeCosmosDbService
            {
                DummyArticles = DummyData.ReturnDummyArticles()
            };

            // Act
            var LArticles = new Articles(LFakeCosmosService);
            var LResult = await LArticles.GetAllArticles();

            // Assert
            LResult.Count().Should().Be(6);

        }

        [Fact]
        public async Task GetSingleArticle_Test() 
        {

            // Arrange
            var LFakeCosmosService = new FakeCosmosDbService
            {
                DummyArticles = DummyData.ReturnDummyArticles()
            };

            // Act
            var LArticles = new Articles(LFakeCosmosService);
            var LResult = await LArticles.GetSingleArticle("f29306f9-36fe-4935-8f86-fb448a21019c");

            // Assert
            LResult.Should().NotBeNull();
            LResult.Title.Should().Be("ZXC");

        }

    }

}
