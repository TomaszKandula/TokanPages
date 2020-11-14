using Xunit;
using FluentAssertions;
using System;
using System.IO;
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
            var LTestFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\__tests\\dummy.txt";
            if (!File.Exists(LTestFilePath)) 
            {
                using var TestFile = new StreamWriter(LTestFilePath, true);
                TestFile.WriteLine("Lorem ipsum dolor sit amet, consectetur adipiscing elit.");
            }

            // Act
            var LResult = await LAzureStorageService.UploadTextFile("tokanpages\\__tests", "dummy.txt", $"{LTestFilePath}");

            // Arrange
            LResult.IsSucceeded.Should().BeTrue();

        }

        [Fact]
        public async Task RemoveFromStorage_Test() 
        {

            // Arrange
            var LAzureStorageService = new AzureStorageService(FAzureStorage);

            // Act
            var LResult = await LAzureStorageService.RemoveFromStorage("tokanpages\\__tests", "dummy.txt");

            // Arrange
            LResult.IsSucceeded.Should().BeTrue();

        }

    }

}
