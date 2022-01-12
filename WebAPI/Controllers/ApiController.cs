using Microsoft.AspNetCore.Mvc;
using Core.Utilities.Results.Concrete;
using Core.Utilities.WebAPI;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult SayHello()
        {
            return new OkObjectResult("Welcome to My API - Emre Ekerbiçer");
        }
    }
}
