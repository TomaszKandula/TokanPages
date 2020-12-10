using Microsoft.AspNetCore.Mvc;
using TokanPages.Logic;
using TokanPages.AppLogger;

namespace TokanPages.Controllers
{

    public class BaseController : ControllerBase
    {

        protected readonly ILogicContext FLogicContext;
        protected readonly IAppLogger    FAppLogger;

        public BaseController(ILogicContext ALogicContext, IAppLogger AAppLogger) 
        {
            FLogicContext = ALogicContext;
            FAppLogger    = AAppLogger;
        }

    }

}
