using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using TokanPages.Backend.Storage.Models;
using TokanPages.Backend.Storage.Settings;

namespace TokanPages.Backend.Storage
{

    public class AzureStorageService : AzureStorageObject, IAzureStorageService
    {

        private readonly AzureStorageSettings FAzureStorage;
        private readonly CloudStorageAccount  FStorageAccount;
        private readonly StorageCredentials   FStorageCredentials;

        public AzureStorageService(AzureStorageSettings AAzureStorage) 
        {
            FAzureStorage       = AAzureStorage;
            FStorageCredentials = new StorageCredentials(FAzureStorage.AccountName, FAzureStorage.AccountKey);
            FStorageAccount     = new CloudStorageAccount(FStorageCredentials, useHttps: true);
        }

        public AzureStorageService() 
        { 
        }

        public override string GetBaseUrl { get => FAzureStorage.BaseUrl.Replace("{AccountName}", FAzureStorage.AccountName); }

        public override async Task<ActionResult> UploadFile(string ADestContainerName, string ADestFileName, string ASrcFullFilePath, string AContentType, CancellationToken ACancellationToken) 
        {

            try 
            {

                var LBlobClient = FStorageAccount.CreateCloudBlobClient();
                var LContainer  = LBlobClient.GetContainerReference(ADestContainerName);
                var LBlockBlob  = LContainer.GetBlockBlobReference(ADestFileName);
                var LFileStream = File.OpenRead(ASrcFullFilePath);

                LBlockBlob.Properties.ContentType = AContentType;
                await LBlockBlob.UploadFromStreamAsync(LFileStream, null, null, null, ACancellationToken);

                return new ActionResult 
                { 
                    IsSucceeded = true
                };

            }
            catch (Exception LException)
            {
                return new ActionResult 
                { 
                    ErrorCode = LException.HResult.ToString(),
                    ErrorDesc = LException.Message
                };
            }

        }

        public override async Task<ActionResult> RemoveFromStorage(string AContainerName, string AFileName, CancellationToken ACancellationToken) 
        {

            try
            {

                var LBlobClient = FStorageAccount.CreateCloudBlobClient();
                var LContainer  = LBlobClient.GetContainerReference(AContainerName);
                var LBlockBlob  = LContainer.GetBlockBlobReference(AFileName);

                await LBlockBlob.DeleteIfExistsAsync(DeleteSnapshotsOption.None, null, null, null, ACancellationToken);

                return new ActionResult
                {
                    IsSucceeded = true
                };

            }
            catch (Exception LException)
            {
                return new ActionResult
                {
                    ErrorCode = LException.HResult.ToString(),
                    ErrorDesc = LException.Message
                };
            }

        }

    }

}
