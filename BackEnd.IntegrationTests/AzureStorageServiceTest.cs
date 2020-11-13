using Xunit;
using FluentAssertions;
using System.Threading.Tasks;
using TokanPages.BackEnd.Storage;
using TokanPages.BackEnd.Settings;
using Microsoft.Extensions.Configuration;

namespace BackEnd.IntegrationTests
{

    public class AzureStorageServiceTest
    {

        private readonly AzureStorage FAzureStorage;

        public AzureStorageServiceTest()
        {

            var Configuration = new ConfigurationBuilder()
                .AddUserSecrets<AzureStorageServiceTest>()
                .Build();

            FAzureStorage = Configuration.GetSection("AzureStorage").Get<AzureStorage>();

        }

        [Fact]
        public async Task UploadTextFile_Test() 
        {

            // Arrange
            var LAzureStorageService = new AzureStorageService(FAzureStorage);

            // Act
            var LResult = await LAzureStorageService.UploadTextFile("tokanpages\\test", "test.txt", "I:\\Test.txt");

            // Arrange
            LResult.IsSucceeded.Should().BeTrue();

        }

        [Fact]
        public async Task RemoveFromStorage_Test() 
        {

            // Arrange
            var LAzureStorageService = new AzureStorageService(FAzureStorage);

            // Act
            var LResult = await LAzureStorageService.RemoveFromStorage("tokanpages\\test", "test.txt");

            // Arrange
            LResult.IsSucceeded.Should().BeTrue();

        }

    }

}
