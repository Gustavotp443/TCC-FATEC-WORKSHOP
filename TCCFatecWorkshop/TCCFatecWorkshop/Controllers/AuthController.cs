using Microsoft.AspNetCore.Mvc;
using TCCFatecWorkshop.Models;
using TCCFatecWorkshop.Models.DTO.User;
using TCCFatecWorkshop.Repositories.Exceptions;
using TCCFatecWorkshop.Repositories.Interfaces;
using TCCFatecWorkshop.Services;

namespace TCCFatecWorkshop.Controllers
{
    [ApiController]
    [Route("v1/auth")]
    public class AuthController : Controller
    {
        
        private readonly IUserRepository _userRepository;

        public AuthController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }



        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                var user = await _userRepository.GetByEmailAndPassword(loginDTO.Email, loginDTO.Password);
                if (user==null)
                {
                    return Unauthorized();
                }
                var token = TokenService.GenerateToken(user);
                return Ok(new { token });

            
            }catch(LoginFailed ex)
            {
                return BadRequest(new { login = ex.Message});
            }
            }
            

    }
}
