using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using TokanPages.Backend.Shared;
using TokanPages.Backend.Storage.Models;
using TokanPages.Backend.Core.Exceptions;
using TokanPages.Backend.Core.Extensions;
using TokanPages.Backend.Shared.Resources;

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
        
        public async Task<StorageByteContentModel> ReadAllBytes(string ASourceFilePath)
        {
            var LBlob = FContainer.GetBlockBlobReference(ASourceFilePath);

            if (!await LBlob.ExistsAsync())
                return null;

            var LResult = new byte[LBlob.Properties.Length];
            await LBlob.DownloadToByteArrayAsync(LResult, 0);
            var LContentType = LBlob.Properties.ContentType;

            return new StorageByteContentModel
            {
                Content = LResult,
                ContentType = LContentType
            };
        }

        public async Task<StorageStreamContentModel> OpenRead(string ASourceFilePath)
        {
            var LBlob = FContainer.GetBlockBlobReference(ASourceFilePath);
            var LStream = await LBlob.OpenReadAsync();
            var LContentType = LBlob.Properties.ContentType;

            return new StorageStreamContentModel
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

        public async Task UploadText(Guid AId, string ATextToUpload)
        {
            var LTextToBase64 = ATextToUpload.ToBase64Encode();
            var LBytes = Convert.FromBase64String(LTextToBase64);
            var LContents = new MemoryStream(LBytes);

            try
            {
                var LDestinationPath = $"content\\articles\\{AId.ToString().ToLower()}\\text.json";
                await UploadFile(LContents, LDestinationPath);
            }
            catch (Exception LException)
            {
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LException.Message);
            }
        }

        public async Task UploadImage(Guid AId, string AImageToUpload)
        {
            if (!AImageToUpload.IsBase64String()) 
                throw new BusinessException(nameof(ErrorCodes.INVALID_BASE64), ErrorCodes.INVALID_BASE64);
            
            var LBytes = Convert.FromBase64String(AImageToUpload);
            var LContents = new MemoryStream(LBytes);
            
            try
            {
                var LDestinationPath = $"content\\articles\\{AId.ToString().ToLower()}\\image.jpeg";
                await UploadFile(LContents, LDestinationPath);
            }
            catch (Exception LException)
            {
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LException.Message);
            }
        }
    }
}
