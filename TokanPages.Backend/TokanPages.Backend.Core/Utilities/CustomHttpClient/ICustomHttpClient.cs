namespace TokanPages.Backend.Core.Utilities.CustomHttpClient
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public interface ICustomHttpClient
    {
        Task<Results> Execute(Configuration AConfiguration, CancellationToken ACancellationToken = default);

        ContentResult GetContentResult(int? AStatusCode, string AContent, string AContentType);

        string SetAuthentication(string ALogin, string APassword);

        string SetAuthentication(string AToken);

        string GetFirstEmptyParameterName(IDictionary<string, string> AParameterList);
    }
}