using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCCFatecWorkshop.Models;

namespace TCCFatecWorkshop.Controllers
{
    [Route("[controller]")]     //portadaapi/user
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<User>> FindAllUser() 
        {
            return Ok();
        }
    }
}
