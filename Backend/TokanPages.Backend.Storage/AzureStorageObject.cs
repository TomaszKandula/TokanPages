using System.Threading.Tasks;
using TokanPages.Backend.Shared.Models;

namespace TokanPages.Backend.Storage
{

    public abstract class AzureStorageObject
    {
        public abstract string GetBaseUrl { get; }
        public abstract Task<ActionResult> UploadTextFile(string ADestContainerName, string ADestFileName, string ASrcFullFilePath);
        public abstract Task<ActionResult> RemoveFromStorage(string AContainerName, string AFileName);
    }

}
