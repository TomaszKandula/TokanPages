﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace TokanPages.Backend.Core.Services.FileUtility
{

    public class FileUtility : IFileUtility
    {

        public virtual async Task<string> SaveToFile(string ATemporaryDir, string AFileName, string ATextContent) 
        {

            var LTempFileName = $"{AFileName}";

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

        public virtual async Task<string> GetFileFromUrl(string AUrl, CancellationToken ACancellationToken)
        {
            using var LHttpClient = new HttpClient();
            var LResponse = await LHttpClient.GetAsync(AUrl, ACancellationToken);
            return await LResponse.Content.ReadAsStringAsync();
        }

    }

}
