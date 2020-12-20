using System.Threading;
using System.Threading.Tasks;
using TokanPages.Backend.Core.Services.AppLogger;
using MediatR;

namespace TokanPages.Backend.Core.Behaviours
{
    
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {

        private readonly ILogger FLogger;

        public LoggingBehaviour(ILogger ALogger) 
        {
            FLogger = ALogger;
        }

        public async Task<TResponse> Handle(TRequest ARequest, CancellationToken ACancellationToken, RequestHandlerDelegate<TResponse> ANext)
        {
            FLogger.LogInfo($"Begin: Handle {typeof(TRequest).Name}");
            var LResponse = await ANext();
            FLogger.LogInfo($"Finish: Handle {typeof(TResponse).Name}");
            return LResponse;
        }
    }

}
