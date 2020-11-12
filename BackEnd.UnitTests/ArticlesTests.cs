using Xunit;
using FluentAssertions;
using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.UnitTests.Mock;
using BackEnd.UnitTests.Database;
using TokanPages.BackEnd.Logic.Articles;
using TokanPages.BackEnd.Controllers.Articles.Model;

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
            LResult.Should().NotBeNull();
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
            var LResult1 = await LArticles.GetSingleArticle("f29306f9-36fe-4935-8f86-fb448a21019c");
            var LResult2 = await LArticles.GetSingleArticle(" ");

            // Assert
            LResult1.Should().NotBeNull();
            LResult1.Title.Should().Be("ZXC");
            LResult2.Should().BeNull();

        }

        [Fact]
        public async Task AddNewArticle_Test() 
        {

            // Arrange
            var LFakeCosmosService = new FakeCosmosDbService
            {
                DummyArticles = DummyData.ReturnDummyArticles()
            };

            var LPayLoad = new ArticleRequest 
            { 
                Title  = "Records in C# 9.0",
                Desc   = "New feature...",
                Status = "draft",
                Text   = "In new C# 9.0..."
            };

            // Act
            var LArticles = new Articles(LFakeCosmosService);
            var LResult = await LArticles.AddNewArticle(LPayLoad);

            // Assert
            LResult.Should().NotBeNullOrEmpty();
            var IsGuid = Guid.TryParse(LResult, out _);
            IsGuid.Should().BeTrue();

        }

        [Fact]
        public async Task UpdateArticle_Test()
        {

            // Arrange
            var LFakeCosmosService = new FakeCosmosDbService
            {
                DummyArticles = DummyData.ReturnDummyArticles()
            };

            var LPayLoad = new ArticleRequest
            {
                Id     = "66ab39af-424c-454d-a942-1c2977632fb8",
                Title  = "QWERTY",
                Desc   = "New feature...",
                Status = "draft",
                Likes  = 0,
                Text   = "New feature..."
            };

            // Act
            var LArticles = new Articles(LFakeCosmosService);
            var LResult = await LArticles.UpdateArticle(LPayLoad);

            // Assert
            LResult.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public async Task DeleteArticle_Test() 
        {

            // Arrange
            var LFakeCosmosService = new FakeCosmosDbService
            {
                DummyArticles = DummyData.ReturnDummyArticles()
            };

            // Act
            var LArticles = new Articles(LFakeCosmosService);
            var LResult1 = await LArticles.DeleteArticle("4d9b0aad-7b69-4f12-a5cf-7308f33cffd0");
            var LResult2 = await LArticles.DeleteArticle("this is strange id");

            // Assert
            LResult1.Should().NotBeNull();
            LResult1.Should().Be(HttpStatusCode.NoContent);

            LResult2.Should().NotBeNull();
            LResult2.Should().Be(HttpStatusCode.NotFound);

        }

    }

}
