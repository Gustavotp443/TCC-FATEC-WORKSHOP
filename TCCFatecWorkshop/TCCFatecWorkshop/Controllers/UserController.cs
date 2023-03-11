using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCCFatecWorkshop.Models;
using TCCFatecWorkshop.Models.DTO.User;
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

      


        [Authorize(Policy = "UserPolicy")]
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
            } catch(UsernameAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }
          
        }

        [Authorize(Policy = "UserPolicy")]
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


                var user = new User();


                if (!string.IsNullOrEmpty(userUpdateDTO.Username)) user.Username = userUpdateDTO.Username;
            
                if (!string.IsNullOrEmpty(userUpdateDTO.Password)) user.Password = userUpdateDTO.Password;

                if(string.IsNullOrEmpty(userUpdateDTO.Username) && string.IsNullOrEmpty(userUpdateDTO.Password))
                {
                    return BadRequest("Os dois campos não podem estar nulos");

                }


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

        [Authorize(Policy = "UserPolicy")]
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
