using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCCFatecWorkshop.Models.DTO.User;
using TCCFatecWorkshop.Models;
using TCCFatecWorkshop.Repositories.Exceptions;
using TCCFatecWorkshop.Repositories;
using TCCFatecWorkshop.Repositories.Interfaces;
using TCCFatecWorkshop.Models.DTO.Workshop;
using Microsoft.AspNetCore.Authorization;

namespace TCCFatecWorkshop.Controllers
{
    [Route("User/{userId}/[controller]")]
    [ApiController]
    public class WorkshopController : ControllerBase
    {
        private readonly IWorkshopRepository _workshopRepository;

        public WorkshopController(IWorkshopRepository workshopRepository)
        {
            _workshopRepository = workshopRepository;
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpGet("{workshopId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Workshop>> FindById(int workshopId)
        {
            try
            {
                var userIdClaim = HttpContext.User.FindFirst("userId")?.Value;
                if (!int.TryParse(userIdClaim, out int userId))
                {
                    return BadRequest("Invalid userId claim");
                }

                Workshop workshop = await _workshopRepository.FindById(userId,workshopId);
                return Ok(new
                {
                    name = workshop.Name,
                    email = workshop.Email,
                    description = workshop.Description,
                    CreatedAtAction = workshop.CreatedAt,
                    updatedat = workshop.UpdatedAt,
                    user = workshop.User.Username
                }); ;
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }

        [Authorize(Policy = "UserPolicy")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Workshop>> Create([FromBody] WorkshopDTO workshopDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userIdClaim = HttpContext.User.FindFirst("userId")?.Value;
                if (!int.TryParse(userIdClaim, out int userId))
                {
                    return BadRequest("Invalid userId claim");
                }

                var workshop = new Workshop
                {
                    Name = workshopDTO.Name,
                    UserId = userId
                };

                if (!string.IsNullOrEmpty(workshopDTO.Email)) workshop.Email = workshopDTO.Email;
                if (!string.IsNullOrEmpty(workshopDTO.Description)) workshop.Description = workshopDTO.Description;


                Workshop workshopReturned = await _workshopRepository.Create(userId, workshop);

                return Created($"User/{userId}/workshop/{workshopReturned.Id}", new
                {
                    workshopReturned.Name,
                    workshopReturned.Email,
                    workshopReturned.Description,
                });
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NameAlreadyExistsException ex)
            {
                return BadRequest(ex.Message);
            }   
        }

        [Authorize(Policy = "UserPolicy")]
        [HttpDelete("{workshopId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Workshop>> Delete(int workshopId)
        {
            try
            {
                var userIdClaim = HttpContext.User.FindFirst("userId")?.Value;
                if (!int.TryParse(userIdClaim, out int userId))
                {
                    return BadRequest("Invalid userId claim");
                }

                await _workshopRepository.Delete(userId, workshopId);
                return NoContent();

            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }



        [Authorize(Policy = "UserPolicy")]
        [HttpPut("{workshopId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Workshop>> Update(int workshopId,[FromBody] WorkshopUpdateDTO workshopUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userIdClaim = HttpContext.User.FindFirst("userId")?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
            {
                return BadRequest("Invalid userId claim");
            }

            try
            {

                var workshop = await _workshopRepository.FindById(userId, workshopId);
                if (workshop == null)
                {
                    return NotFound("Workshop not found");
                }

                if(!string.IsNullOrEmpty(workshopUpdateDTO.Name)) workshop.Name = workshopUpdateDTO.Name;
                if (!string.IsNullOrEmpty(workshopUpdateDTO.Email)) workshop.Email = workshopUpdateDTO.Email;
                if (!string.IsNullOrEmpty(workshopUpdateDTO.Description)) workshop.Description = workshopUpdateDTO.Description;

                var updatedWorkshop = await _workshopRepository.Update(userId, workshop, workshopId);

                return Ok(new
                {
                    name = updatedWorkshop.Name,
                    email = updatedWorkshop.Email,
                    description = updatedWorkshop.Description,
                    User = updatedWorkshop.User.Username
                });
            
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }

        }



    }
}
