namespace TokanPages.Backend.Core.Utilities.CustomHttpClient
{
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Models;

    public interface ICustomHttpClient
    {
        Task<Results> Execute(Configuration AConfiguration, CancellationToken ACancellationToken = default);

        string SetAuthentication(string ALogin, string APassword);

        string SetAuthentication(string AToken);

        string GetFirstEmptyParameterName(IDictionary<string, string> AParameterList);
    }
}