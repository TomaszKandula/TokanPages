using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TokanPages.BackEnd.Logic;
using TokanPages.BackEnd.AppLogger;

namespace TokanPages.BackEnd.Controllers
{

    public class BaseController : ControllerBase
    {

        protected readonly IConfiguration FConfiguration;
        protected readonly ILogicContext  FLogicContext;
        protected readonly IAppLogger     FAppLogger;

        public BaseController(
            IConfiguration AConfiguration,
            ILogicContext  ALogicContext, 
            IAppLogger     AAppLogger 
        ) 
        {
            FConfiguration = AConfiguration;
            FLogicContext  = ALogicContext;
            FAppLogger     = AAppLogger;
        }

    }

}
