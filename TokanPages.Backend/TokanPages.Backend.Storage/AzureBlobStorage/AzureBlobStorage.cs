namespace TokanPages.Backend.Storage.AzureBlobStorage
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Models;
    using Models;
    using Shared;
    using Core.Exceptions;
    using Core.Extensions;
    using Shared.Resources;

    public class AzureBlobStorage : IAzureBlobStorage
    {
        private readonly BlobContainerClient FContainer;

        public AzureBlobStorage(string AConnectionString, string AContainerName)
        {
            var LBlobServiceClient = new BlobServiceClient(AConnectionString);
            FContainer = LBlobServiceClient.GetBlobContainerClient(AContainerName);
        }

        public async Task<StorageByteContent> ReadAllBytes(string ASourceFilePath, CancellationToken ACancellationToken)
        {
            if (string.IsNullOrEmpty(ASourceFilePath))
                throw new AggregateException($"Argument '{nameof(ASourceFilePath)}' cannot be null or empty.");

            var LBlobClient = FContainer.GetBlobClient(ASourceFilePath);

            if (!await LBlobClient.ExistsAsync(ACancellationToken))
                return null;

            var LProperties = await LBlobClient.GetPropertiesAsync(cancellationToken: ACancellationToken);
            var LContentType = LProperties.Value.ContentType;

            var LResponse = await LBlobClient.DownloadContentAsync(ACancellationToken);
            var LContent = LResponse.Value.Content;

            return new StorageByteContent
            {
                Content = LContent,
                ContentType = LContentType
            };
        }

        public async Task<StorageStreamContent> OpenRead(string ASourceFilePath, CancellationToken ACancellationToken)
        {
            if (string.IsNullOrEmpty(ASourceFilePath))
                throw new AggregateException($"Argument '{nameof(ASourceFilePath)}' cannot be null or empty.");

            var LBlob = FContainer.GetBlobClient(ASourceFilePath);
            var LStream = await LBlob.OpenReadAsync(cancellationToken: ACancellationToken);
            var LProperties = await LBlob.GetPropertiesAsync(cancellationToken: ACancellationToken);

            return new StorageStreamContent
            {
                Content = LStream,
                ContentType = LProperties.Value.ContentType
            };
        }

        public async Task UploadFile(Stream ASourceStream, string ADestinationPath, CancellationToken ACancellationToken, string AContentType = Constants.ContentTypes.STREAM)
        {
            if (string.IsNullOrEmpty(ADestinationPath))
                throw new AggregateException($"Argument '{nameof(ADestinationPath)}' cannot be null or empty.");

            var LBlobClient = FContainer.GetBlobClient(ADestinationPath);
            var LBlobHttpHeaders = new BlobHttpHeaders { ContentType = AContentType };
            await LBlobClient.UploadAsync(ASourceStream, LBlobHttpHeaders, cancellationToken: ACancellationToken);
        }

        public async Task<string> GetFileContentType(string ASourceFilePath, CancellationToken ACancellationToken)
        {
            if (string.IsNullOrEmpty(ASourceFilePath))
                throw new AggregateException($"Argument '{nameof(ASourceFilePath)}' cannot be null or empty.");

            var LBlobClient = FContainer.GetBlobClient(ASourceFilePath);
            var LProperties = await LBlobClient.GetPropertiesAsync(cancellationToken: ACancellationToken);

            return LProperties.Value.ContentType;
        }

        public async Task<bool> DeleteFile(string ASourceFilePath, CancellationToken ACancellationToken)
        {
            if (string.IsNullOrEmpty(ASourceFilePath))
                throw new AggregateException($"Argument '{nameof(ASourceFilePath)}' cannot be null or empty.");
            
            return await FContainer
                .GetBlobClient(ASourceFilePath)
                .DeleteIfExistsAsync(cancellationToken: ACancellationToken);
        }

        public async Task UploadContent(string AContent, string ADestinationPath, CancellationToken ACancellationToken)
        {
            if (string.IsNullOrEmpty(AContent))
                throw new ArgumentException($"Argument '{nameof(AContent)}' cannot be null or empty.");
            
            if (string.IsNullOrEmpty(ADestinationPath))
                throw new ArgumentException($"Argument '{nameof(ADestinationPath)}' cannot be null or empty.");
            
            var LToUpload = AContent;
            if (!AContent.IsBase64String())
                LToUpload = AContent.ToBase64Encode();

            var LBytes = Convert.FromBase64String(LToUpload);
            var LContents = new MemoryStream(LBytes);

            try
            {
                await UploadFile(LContents, ADestinationPath, ACancellationToken);
            }
            catch (Exception LException)
            {
                throw new BusinessException(nameof(ErrorCodes.CANNOT_SAVE_TO_AZURE_STORAGE), LException.Message);
            }
        }
    }
}