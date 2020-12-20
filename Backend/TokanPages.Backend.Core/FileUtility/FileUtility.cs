using System;
using System.IO;
using System.Threading.Tasks;

namespace TokanPages.Backend.Core.FileUtility
{

    public class FileUtility : IFileUtility
    {

        public async Task<string> SaveToFile(string ATemporaryDir, string AFileName, string ATextContent) 
        {

            var LTempFileName = $"{AFileName}.txt";

            var LBaseDirectory = AppDomain.CurrentDomain.BaseDirectory + $"\\{ATemporaryDir}";
            if (!Directory.Exists(LBaseDirectory))
            {
                Directory.CreateDirectory(LBaseDirectory);
            }

            var LTempFilePath = LBaseDirectory + $"\\{LTempFileName}";
            if (!File.Exists(LTempFilePath))
            {
                using var LFileToUpload = new StreamWriter(LTempFilePath, true);
                await LFileToUpload.WriteAsync(ATextContent);
            }

            return LTempFilePath;

        }

    }

}
