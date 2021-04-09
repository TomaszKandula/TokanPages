using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace TokanPages.Backend.Storage.AzureBlobStorage
{
    public class AzureBlobStorage : IAzureBlobStorage
    {
        private readonly CloudBlobContainer FContainer;

        public AzureBlobStorage(string AConnectionString, string AContainerName)
            => FContainer = CloudStorageAccount
                .Parse(AConnectionString)
                .CreateCloudBlobClient()
                .GetContainerReference(AContainerName);
        
        
        public async Task<StorageByteContent> ReadAllBytesAsync(string AFilePath)
        {
            var LBlob = FContainer.GetBlockBlobReference(AFilePath);

            if (!await LBlob.ExistsAsync())
                return null;

            var LResult = new byte[LBlob.Properties.Length];
            await LBlob.DownloadToByteArrayAsync(LResult, 0);
            var LContentType = LBlob.Properties.ContentType;

            return new StorageByteContent
            {
                Content = LResult,
                ContentType = LContentType
            };
        }

        public async Task<StorageStreamContent> OpenReadAsync(string AFilePath)
        {
            var LBlob = FContainer.GetBlockBlobReference(AFilePath);
            var LStream = await LBlob.OpenReadAsync();
            var LContentType = LBlob.Properties.ContentType;

            return new StorageStreamContent
            {
                Content = LStream,
                ContentType = LContentType
            };
        }

        public async Task UploadFileAsync(Stream AStream, string AFilePath, string AContentType, long AMaxLength)
        {
            var LBlob = FContainer.GetBlockBlobReference(AFilePath);
            LBlob.Properties.ContentType = AContentType;

            var LMaxSizeCondition = new AccessCondition { IfMaxSizeLessThanOrEqual = AMaxLength };
            var LMaxSizeOption = new BlobRequestOptions { SingleBlobUploadThresholdInBytes = AMaxLength };

            var LOperationContext = new OperationContext();
            await LBlob.UploadFromStreamAsync(AStream, LMaxSizeCondition, LMaxSizeOption, LOperationContext);
        }

        public async Task UploadFileAsync(Stream AStream, string ADestinationFilePath, string AContentType = "application/octet-stream")
        {
            var LBlob = FContainer.GetBlockBlobReference(ADestinationFilePath);
            LBlob.Properties.ContentType = AContentType;
            await LBlob.UploadFromStreamAsync(AStream);
        }

        public async Task<string> GetFileContentType(string AFilePath)
        {
            var LBlockBlobReference = FContainer.GetBlockBlobReference(AFilePath);
            await LBlockBlobReference.FetchAttributesAsync();

            return LBlockBlobReference.Properties.ContentType;
        }

        public async Task<bool> DeleteFileAsync(string AFilePath)
            => await FContainer.GetBlockBlobReference(AFilePath)
                .DeleteIfExistsAsync();
    }
}
