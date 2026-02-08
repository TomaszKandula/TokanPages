using System.Diagnostics.CodeAnalysis;
using TokanPages.Services.HttpClientService.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;
using TokanPages.Backend.Utility.Abstractions;

namespace TokanPages.Backend.Application;

[ExcludeFromCodeCoverage]
public abstract class RangeRequestHandler <TRequest, TResult> : IRequestHandler<TRequest, TResult> where TRequest : IRequest<TResult>
{
    protected readonly IHttpContextAccessor HttpContextAccessor;

    protected readonly IHttpClientServiceFactory ClientServiceFactory;

    protected readonly ILoggerService LoggerService;

    protected RangeRequestHandler(IHttpContextAccessor httpContextAccessor, 
        IHttpClientServiceFactory clientServiceFactory, ILoggerService loggerService)
    {
        HttpContextAccessor = httpContextAccessor;
        ClientServiceFactory = clientServiceFactory;
        LoggerService = loggerService;
    }

    public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
}