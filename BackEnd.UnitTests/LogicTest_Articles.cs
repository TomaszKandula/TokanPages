using Xunit;
using FluentAssertions;
using System;
using System.Net;
using System.Linq;
using System.Threading.Tasks;
using TokanPages.BackEnd.Settings;
using TokanPages.BackEnd.Database;
using TokanPages.BackEnd.Logic.Articles;
using TokanPages.BackEnd.Controllers.Articles.Model;
using ArticlesModel = TokanPages.BackEnd.Database.Model.Articles;
using BackEnd.UnitTests.Mocks.CosmosDb;

namespace BackEnd.UnitTests
{

    public class LogicTest_Articles
    {

        [Fact]
        public async Task Should_GetAllArticles()
        {

            // Arrange
            var LDbConfig = new CosmosDb() 
            {
                DatabaseName = CosmosDbEmulator.DatabaseName,
                Account = CosmosDbEmulator.Account,
                Key = CosmosDbEmulator.Key
            };
            var LCosmosService = new CosmosDbService(LDbConfig);
            LCosmosService.InitContainer<ArticlesModel>();

            // Act
            var LArticles = new Articles(LCosmosService);
            var LResult = await LArticles.GetAllArticles();

            // Assert
            LResult.Should().NotBeNull();
            LResult.Count().Should().BeGreaterThan(0);

        }

        [Fact]
        public async Task Should_GetOneArticle() 
        {

            // Arrange
            var LDbConfig = new CosmosDb()
            {
                DatabaseName = CosmosDbEmulator.DatabaseName,
                Account = CosmosDbEmulator.Account,
                Key = CosmosDbEmulator.Key
            };
            var LCosmosService = new CosmosDbService(LDbConfig);
            LCosmosService.InitContainer<ArticlesModel>();

            // Act
            var LArticles = new Articles(LCosmosService);
            var LResult1 = await LArticles.GetSingleArticle("80cc8b7b-56f6-4e9d-8e17-0dc010b892d2");
            var LResult2 = await LArticles.GetSingleArticle(" ");

            // Assert
            LResult1.Should().NotBeNull();
            LResult1.Title.Should().Be("ABC");
            LResult1.Desc.Should().Be("Lorem ipsum...");
            LResult1.Status.Should().Be("draft");
            LResult1.Likes.Should().Be(0);
            LResult1.ReadCount.Should().Be(0);
            LResult2.Should().BeNull();

        }

        [Fact]
        public async Task Should_AddNewArticle() 
        {

            // Arrange
            var LDbConfig = new CosmosDb()
            {
                DatabaseName = CosmosDbEmulator.DatabaseName,
                Account = CosmosDbEmulator.Account,
                Key = CosmosDbEmulator.Key
            };
            var LCosmosService = new CosmosDbService(LDbConfig);
            LCosmosService.InitContainer<ArticlesModel>();

            var LPayLoad = new ArticleRequest 
            { 
                Title  = "Records in C# 9.0",
                Desc   = "New feature...",
                Status = "draft",
                Text   = "In new C# 9.0..."
            };

            // Act
            var LArticles = new Articles(LCosmosService);
            var LResult = await LArticles.AddNewArticle(LPayLoad);

            // Assert
            LResult.Should().NotBeNullOrEmpty();
            var IsGuid = Guid.TryParse(LResult, out _);
            IsGuid.Should().BeTrue();

        }

        [Fact]
        public async Task Should_UpdateArticle()
        {

            // Arrange
            var LDbConfig = new CosmosDb()
            {
                DatabaseName = CosmosDbEmulator.DatabaseName,
                Account = CosmosDbEmulator.Account,
                Key = CosmosDbEmulator.Key
            };
            var LCosmosService = new CosmosDbService(LDbConfig);
            LCosmosService.InitContainer<ArticlesModel>();

            var LPayLoad = new ArticleRequest
            {
                Id     = "66ab39af-424c-454d-a942-1c2977632fb8",
                Title  = "QWERTY",
                Desc   = "New feature...",
                Status = "draft",
                Likes  = 0,
                ReadCount = 100,
                Text   = "New feature..."
            };

            // Act
            var LArticles = new Articles(LCosmosService);
            var LResult = await LArticles.UpdateArticle(LPayLoad);

            // Assert
            LResult.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public async Task Should_DeleteArticle() 
        {

            // Arrange
            var LDbConfig = new CosmosDb()
            {
                DatabaseName = CosmosDbEmulator.DatabaseName,
                Account = CosmosDbEmulator.Account,
                Key = CosmosDbEmulator.Key
            };
            var LCosmosService = new CosmosDbService(LDbConfig);
            LCosmosService.InitContainer<ArticlesModel>();

            // Act
            var LArticles = new Articles(LCosmosService);
            var LResult = await LArticles.DeleteArticle("this is strange id");

            // Assert
            LResult.Should().NotBeNull();
            LResult.Should().Be(HttpStatusCode.NotFound);

        }

    }

}
