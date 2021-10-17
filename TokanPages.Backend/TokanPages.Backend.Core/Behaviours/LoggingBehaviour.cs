namespace TokanPages.Backend.Core.Behaviours
{   
    using System.Threading;
    using System.Threading.Tasks;
    using System.Diagnostics.CodeAnalysis;
    using Logger;
    using MediatR;

    [ExcludeFromCodeCoverage]
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger _logger;

        public LoggingBehaviour(ILogger logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation($"Begin: Handle {typeof(TRequest).Name}");
            var response = await next();
            _logger.LogInformation($"Finish: Handle {typeof(TResponse).Name}");
            return response;
        }
    }
}