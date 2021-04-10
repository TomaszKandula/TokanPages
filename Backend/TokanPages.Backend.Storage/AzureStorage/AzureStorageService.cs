using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using TokanPages.Backend.Storage.Models;
using TokanPages.Backend.Storage.Settings;

namespace TokanPages.Backend.Storage.AzureStorage
{
    public class AzureStorageService : AzureStorageObject, IAzureStorageService
    {
        private readonly AzureStorageSettings FAzureStorageSettings;
        private readonly CloudStorageAccount FCloudStorageAccount;

        public AzureStorageService(AzureStorageSettings AAzureStorage) 
        {
            FAzureStorageSettings = AAzureStorage;
            var LStorageCredentials = new StorageCredentials(FAzureStorageSettings.AccountName, FAzureStorageSettings.AccountKey);
            FCloudStorageAccount = new CloudStorageAccount(LStorageCredentials, true);
        }

        public AzureStorageService() { }

        public override string GetBaseUrl =>
            FAzureStorageSettings.BaseUrl
                .Replace("{AccountName}", FAzureStorageSettings.AccountName)
                .Replace("{ContainerName}", FAzureStorageSettings.ContainerName);

        public override async Task<StorageActionResult> UploadFile(string ADestContainerReference, string ADestFileName, string ASrcFullFilePath, string AContentType, CancellationToken ACancellationToken) 
        {
            try 
            {
                var LFullReference = FAzureStorageSettings.ContainerName + "\\" + ADestContainerReference;

                var LBlobClient = FCloudStorageAccount.CreateCloudBlobClient();
                var LContainer  = LBlobClient.GetContainerReference(LFullReference);
                var LBlockBlob  = LContainer.GetBlockBlobReference(ADestFileName);
                var LFileStream = File.OpenRead(ASrcFullFilePath);

                LBlockBlob.Properties.ContentType = AContentType;
                await LBlockBlob.UploadFromStreamAsync(LFileStream, null, null, null, ACancellationToken);

                return new StorageActionResult { IsSucceeded = true };
            }
            catch (Exception LException)
            {
                return new StorageActionResult 
                { 
                    ErrorCode = LException.HResult.ToString(),
                    ErrorDesc = LException.Message
                };
            }
        }

        public override async Task<StorageActionResult> RemoveFromStorage(string AContainerReference, string AFileName, CancellationToken ACancellationToken) 
        {
            try
            {
                var LFullReference = FAzureStorageSettings.ContainerName + "\\" + AContainerReference;

                var LBlobClient = FCloudStorageAccount.CreateCloudBlobClient();
                var LContainer  = LBlobClient.GetContainerReference(LFullReference);
                var LBlockBlob  = LContainer.GetBlockBlobReference(AFileName);

                await LBlockBlob.DeleteIfExistsAsync(DeleteSnapshotsOption.None, null, null, null, ACancellationToken);

                return new StorageActionResult { IsSucceeded = true };
            }
            catch (Exception LException)
            {
                return new StorageActionResult
                {
                    ErrorCode = LException.HResult.ToString(),
                    ErrorDesc = LException.Message
                };
            }
        }
    }
}
