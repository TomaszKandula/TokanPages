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
        private readonly ILogger FLogger;

        public LoggingBehaviour(ILogger ALogger) => FLogger = ALogger;

        public async Task<TResponse> Handle(TRequest ARequest, CancellationToken ACancellationToken, RequestHandlerDelegate<TResponse> ANext)
        {
            FLogger.LogInformation($"Begin: Handle {typeof(TRequest).Name}");
            var LResponse = await ANext();
            FLogger.LogInformation($"Finish: Handle {typeof(TResponse).Name}");
            return LResponse;
        }
    }
}