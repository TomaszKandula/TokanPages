namespace TokanPages.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MediatR;
    
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMediator FMediator;

        public BaseController(IMediator AMediator) => FMediator = AMediator;
    }
}