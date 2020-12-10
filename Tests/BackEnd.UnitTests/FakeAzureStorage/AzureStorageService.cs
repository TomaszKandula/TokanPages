using System.Threading.Tasks;
using TokanPages.Backend.Storage;
using TokanPages.Backend.Storage.Models;

namespace Backend.UnitTests.FakeAzureStorage
{

    public class AzureStorageService : AzureStorageObject, IAzureStorageService
    {

        public AzureStorageService() 
        { 
        }

        public override string GetBaseUrl { get; } = "";

        public override async Task<ActionResult> UploadTextFile(string ADestContainerName, string ADestFileName, string ASrcFullFilePath)
        {

            return await Task.Run(() => 
            {

                if (string.IsNullOrEmpty(ADestContainerName) 
                    || string.IsNullOrEmpty(ADestFileName) 
                    || string.IsNullOrEmpty(ASrcFullFilePath)) 
                {
                    return new ActionResult { IsSucceeded = false };
                }

                return new ActionResult { IsSucceeded = true };

            });

        }

        public override async Task<ActionResult> RemoveFromStorage(string AContainerName, string AFileName)
        {

            return await Task.Run(() =>
            {

                if (string.IsNullOrEmpty(AContainerName) || string.IsNullOrEmpty(AFileName)) 
                {
                    return new ActionResult { IsSucceeded = false };
                }

                return new ActionResult { IsSucceeded = true };

            });

        }

    }

}
