using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using TokanPages.BackEnd.Settings;
using TokanPages.BackEnd.Storage.Model;

namespace TokanPages.BackEnd.Storage
{

    public class AzureStorageService : IAzureStorageService
    {

        private readonly AzureStorage        FAzureStorage;
        private readonly CloudStorageAccount FStorageAccount;
        private readonly StorageCredentials  FStorageCredentials;

        public AzureStorageService(AzureStorage AAzureStorage) 
        {
            FAzureStorage       = AAzureStorage;
            FStorageCredentials = new StorageCredentials(FAzureStorage.AccountName, FAzureStorage.AccountKey);
            FStorageAccount     = new CloudStorageAccount(FStorageCredentials, useHttps: true);
        }

        public AzureStorageService() 
        { 
        }

        public string GetBaseUrl { get => FAzureStorage.BaseUrl.Replace("{AccountName}", FAzureStorage.AccountName); }

        public virtual async Task<ActionResult> UploadTextFile(string ADestContainerName, string ADestFileName, string ASrcFullFilePath) 
        {

            try 
            {

                var LBlobClient = FStorageAccount.CreateCloudBlobClient();
                var LContainer  = LBlobClient.GetContainerReference(ADestContainerName);
                var LBlockBlob  = LContainer.GetBlockBlobReference(ADestFileName);
                var LFileStream = File.OpenRead(ASrcFullFilePath);

                LBlockBlob.Properties.ContentType = "text/html";
                await LBlockBlob.UploadFromStreamAsync(LFileStream);

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

        public virtual async Task<ActionResult> RemoveFromStorage(string AContainerName, string AFileName) 
        {

            try
            {

                var LBlobClient = FStorageAccount.CreateCloudBlobClient();
                var LContainer  = LBlobClient.GetContainerReference(AContainerName);
                var LBlockBlob  = LContainer.GetBlockBlobReference(AFileName);

                await LBlockBlob.DeleteIfExistsAsync();

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
