using Microsoft.AspNetCore.Mvc;
using TokanPages.BackEnd.Logic;
using TokanPages.BackEnd.AppLogger;

namespace TokanPages.BackEnd.Controllers
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
