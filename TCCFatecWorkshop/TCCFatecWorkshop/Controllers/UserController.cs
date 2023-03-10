using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCCFatecWorkshop.Models;
using TCCFatecWorkshop.Models.DTO.User;
using TCCFatecWorkshop.Models.Token;
using TCCFatecWorkshop.Repositories.Exceptions;
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

        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO loginDTO)
        {
            var user = await _userRepository.GetByEmailAndPassword(loginDTO.Email, loginDTO.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = Token.GenerateJwtToken(user);

            return Ok(new { token });
        }



        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<User>> FindById(int id)
        {
            try
            {
                User user = await _userRepository.FindById(id);
                return Ok(new
                {
                    username = user.Username,
                    email = user.Email,
                    CreatedAtAction = user.CreatedAt,
                    updatedat = user.UpdatedAt,
                });
            } catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<User>> Create([FromBody] UserDTO userDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = new User
                {
                    Username = userDTO.Username,
                    Email = userDTO.Email,
                    Password = userDTO.Password,
                };

                User userReturned = await _userRepository.Create(user);

                return Created($"/user/{userReturned.Id}", new
                {
                    userReturned.Username,
                    userReturned.Email
                });
            }
            catch(EmailAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
          
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<User>> Update(int id, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = new User
                {
                    Username = userUpdateDTO.Username,
                    Password = userUpdateDTO.Password
                };

                User userReturned = await _userRepository.Update(user, id);
                return Ok(new
                {
                    username= userReturned.Username,
                    message= "success"
                });
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<User>> Delete(int id)
        {
            try
            {
                await _userRepository.Delete(id);
                return NoContent();

            }catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
       
    }   
}
