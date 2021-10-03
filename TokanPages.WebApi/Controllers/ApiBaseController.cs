namespace TokanPages.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MediatR;
    
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        protected readonly IMediator FMediator;

        public ApiBaseController(IMediator AMediator) => FMediator = AMediator;
    }
}