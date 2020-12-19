using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace TokanPages.Backend.Core.Behaviours
{
    
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {

        public LoggingBehaviour() 
        {        
        }

        public Task<TResponse> Handle(TRequest ARequest, CancellationToken ACancellationToken, RequestHandlerDelegate<TResponse> ANext)
        {
            throw new NotImplementedException();
        }
    }

}
