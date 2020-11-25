using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TokanPages.BackEnd.Logic;
using TokanPages.BackEnd.AppLogger;
using Swashbuckle.AspNetCore.Annotations;
using TokanPages.BackEnd.Shared;
using TokanPages.BackEnd.Helpers.Statics;
using TokanPages.BackEnd.Controllers.Mailer.Model;
using TokanPages.BackEnd.Controllers.Mailer.Model.Responses;
using TokanPages.BackEnd.Logic.Mailer.Model;

namespace TokanPages.BackEnd.Controllers.Subscribers
{

    [Route("api/v1/[controller]")]
    [ApiController]
    [ResponseCache(CacheProfileName = "Standard")]
    public class SubscribersController : ControllerBase
    {

        private readonly ILogicContext FLogicContext;
        private readonly IAppLogger    FAppLogger;

        public SubscribersController(ILogicContext ALogicContext, IAppLogger AAppLogger)
        {
            FLogicContext = ALogicContext;
            FAppLogger    = AAppLogger;
        }

        // get all
        // get by id
        // post
        // patch
        // delete








    }

}
