using Moq;
using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using TokanPages.BackEnd.Database.Model;
using BackEnd.UnitTests.Mock;
using BackEnd.UnitTests.Configuration;

namespace BackEnd.UnitTests
{

    public class ArticlesTests
    {

        [Fact]
        public async Task GetAllArticles_Test() 
        {

            //Arrange
            var LResponse = new FeedResponse<Article>(DummyData.ReturnDummyArticles());

            var LMockDocumentQuery = new Mock<IFakeDocumentQuery<Article>>();
            LMockDocumentQuery
                .SetupSequence(Query => Query.HasMoreResults)
                .Returns(true)
                .Returns(false);

            LMockDocumentQuery
                .Setup(Query => Query.ExecuteNextAsync<Article>(It.IsAny<CancellationToken>()))
                .ReturnsAsync(LResponse);

            var LDocumentClient = new Mock<IDocumentClient>();

            LDocumentClient
                .Setup(Client => Client.CreateDocumentQuery<Article>(It.IsAny<Uri>(), It.IsAny<SqlQuerySpec>(), It.IsAny<FeedOptions>()))
                .Returns(LMockDocumentQuery.Object);

            var LCosmosDatabase = string.Empty;

            var LDocumentsRepository = new DocumentDbRepository<Article>(LDocumentClient.Object);

            //Act
            var LQuery = LDocumentsRepository.GetQueryable("select * from c"); 
            var LResult = await LDocumentsRepository.ExecuteQueryAsync(LQuery);

            //Assert
            LResult.Count().Should().Be(6);

        }

    }

}
