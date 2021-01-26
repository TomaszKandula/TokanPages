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
        private readonly AzureStorageSettings FAzureStorageSettings;
        private readonly CloudStorageAccount  FCloudStorageAccount;
        private readonly StorageCredentials   FStorageCredentials;

        public AzureStorageService(AzureStorageSettings AAzureStorage) 
        {
            FAzureStorageSettings = AAzureStorage;
            FStorageCredentials   = new StorageCredentials(FAzureStorageSettings.AccountName, FAzureStorageSettings.AccountKey);
            FCloudStorageAccount  = new CloudStorageAccount(FStorageCredentials, useHttps: true);
        }

        public AzureStorageService() 
        { 
        }

        public override string GetBaseUrl { get => FAzureStorageSettings.BaseUrl
                .Replace("{AccountName}", FAzureStorageSettings.AccountName)
                .Replace("{ContainerName}", FAzureStorageSettings.ContainerName); }

        public override async Task<ActionResult> UploadFile(string ADestContainerReference, string ADestFileName, string ASrcFullFilePath, string AContentType, CancellationToken ACancellationToken) 
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

        public override async Task<ActionResult> RemoveFromStorage(string AContainerReference, string AFileName, CancellationToken ACancellationToken) 
        {
            try
            {
                var LFullReference = FAzureStorageSettings.ContainerName + "\\" + AContainerReference;

                var LBlobClient = FCloudStorageAccount.CreateCloudBlobClient();
                var LContainer  = LBlobClient.GetContainerReference(LFullReference);
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
