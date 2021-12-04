namespace TokanPages.WebApi.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    [Route("api/v1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProxyBaseController : ControllerBase { }
}