using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace TokanPages.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator FMediator;

        public BaseController(IMediator AMediator) => FMediator = AMediator;
    }
}
