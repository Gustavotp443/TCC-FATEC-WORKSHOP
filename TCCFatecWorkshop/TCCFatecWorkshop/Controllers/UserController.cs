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
        [HttpGet("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<User>> FindById(int userId)
        {
            try
            {
                User user = await _userRepository.FindById(userId);
                return Ok(new
                {
                    username = user.Username,
                    email = user.Email,
                    CreatedAtAction = user.CreatedAt,
                    updatedat = user.UpdatedAt,
                    Workshops = user.Workshops.Select(w => new {
                        name = w.Name,
                        email = w.Email,
                        description = w.Description,
                        CreatedAtAction = w.CreatedAt,
                        updatedat = w.UpdatedAt
                    }).ToList(),
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
        [HttpPut("{userId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<User>> Update(int userId, [FromBody] UserUpdateDTO userUpdateDTO)
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


                User userReturned = await _userRepository.Update(user, userId);
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
        [HttpDelete("{userId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<User>> Delete(int userId)
        {
            try
            {
                await _userRepository.Delete(userId);
                return NoContent();

            }catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
       
    }   
}
