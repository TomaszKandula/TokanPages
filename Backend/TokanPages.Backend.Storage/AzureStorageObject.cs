using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Storage.Models;

namespace TokanPages.Backend.Storage
{

    public abstract class AzureStorageObject
    {
        public abstract string GetBaseUrl { get; }
        public abstract Task<ActionResult> UploadFile(string ADestContainerName, string ADestFileName, string ASrcFullFilePath, string AContentType, CancellationToken ACancellationToken);
        public abstract Task<ActionResult> RemoveFromStorage(string AContainerName, string AFileName, CancellationToken ACancellationToken);
    }

}
