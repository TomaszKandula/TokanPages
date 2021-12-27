namespace TokanPages.Backend.Core.Utilities.CustomHttpClient;

using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;

public interface ICustomHttpClient
{
    Task<Results> Execute(Configuration configuration, CancellationToken cancellationToken = default);

    string SetAuthentication(string login, string password);

    string SetAuthentication(string token);

    string GetFirstEmptyParameterName(IDictionary<string, string> parameterList);
}