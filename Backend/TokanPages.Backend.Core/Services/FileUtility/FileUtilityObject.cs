using System.Threading;
using System.Threading.Tasks;

namespace TokanPages.Backend.Core.Services.FileUtility
{
    public abstract class FileUtilityObject
    {
        public abstract Task<string> SaveToFile(string ATemporaryDir, string AFileName, string ATextContent);
        public abstract Task<string> GetFileFromUrl(string AUrl, CancellationToken ACancellationToken);
    }
}