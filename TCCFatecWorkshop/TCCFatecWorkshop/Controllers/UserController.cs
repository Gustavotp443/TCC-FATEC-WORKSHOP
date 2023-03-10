using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCCFatecWorkshop.Models;
using TCCFatecWorkshop.Repositories.Interfaces;

namespace TCCFatecWorkshop.Controllers
{
    [Route("[controller]")]     //portadaapi/user
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<User>> FindById(int id) 
        {
            User user = await _userRepository.FindById(id);
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create([FromBody] User user)
        {
            User userReturned = await _userRepository.Create(user);
            return Ok(userReturned);
        }
    }
}
