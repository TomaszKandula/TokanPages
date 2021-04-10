using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using TokanPages.Backend.Shared;

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

        public async Task<StorageByteContent> ReadAllBytes(string ASourceFilePath)
        {
            var LBlob = FContainer.GetBlockBlobReference(ASourceFilePath);

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

        public async Task<StorageStreamContent> OpenRead(string ASourceFilePath)
        {
            var LBlob = FContainer.GetBlockBlobReference(ASourceFilePath);
            var LStream = await LBlob.OpenReadAsync();
            var LContentType = LBlob.Properties.ContentType;

            return new StorageStreamContent
            {
                Content = LStream,
                ContentType = LContentType
            };
        }

        public async Task UploadFile(Stream ASourceStream, string ADestinationPath, string AContentType, long AMaxLength)
        {
            var LBlob = FContainer.GetBlockBlobReference(ADestinationPath);
            LBlob.Properties.ContentType = AContentType;

            var LMaxSizeCondition = new AccessCondition { IfMaxSizeLessThanOrEqual = AMaxLength };
            var LMaxSizeOption = new BlobRequestOptions { SingleBlobUploadThresholdInBytes = AMaxLength };

            var LOperationContext = new OperationContext();
            await LBlob.UploadFromStreamAsync(ASourceStream, LMaxSizeCondition, LMaxSizeOption, LOperationContext);
        }

        public async Task UploadFile(Stream ASourceStream, string ADestinationPath, string AContentType = Constants.ContentTypes.STREAM)
        {
            var LBlob = FContainer.GetBlockBlobReference(ADestinationPath);
            LBlob.Properties.ContentType = AContentType;
            await LBlob.UploadFromStreamAsync(ASourceStream);
        }

        public async Task<string> GetFileContentType(string ASourceFilePath)
        {
            var LBlockBlobReference = FContainer.GetBlockBlobReference(ASourceFilePath);
            await LBlockBlobReference.FetchAttributesAsync();

            return LBlockBlobReference.Properties.ContentType;
        }

        public async Task<bool> DeleteFile(string ASourceFilePath)
            => await FContainer
                .GetBlockBlobReference(ASourceFilePath)
                .DeleteIfExistsAsync();
    }
}
