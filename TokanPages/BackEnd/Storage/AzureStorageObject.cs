using System.Threading.Tasks;
using TokanPages.BackEnd.Shared.Models;

namespace TokanPages.BackEnd.Storage
{

    public abstract class AzureStorageObject
    {
        public abstract string GetBaseUrl { get; }
        public abstract Task<ActionResult> UploadTextFile(string ADestContainerName, string ADestFileName, string ASrcFullFilePath);
        public abstract Task<ActionResult> RemoveFromStorage(string AContainerName, string AFileName);
    }

}
