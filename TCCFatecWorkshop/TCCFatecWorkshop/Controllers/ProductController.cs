using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TCCFatecWorkshop.Data;
using TCCFatecWorkshop.Models.DTO.Workshop;
using TCCFatecWorkshop.Models;
using TCCFatecWorkshop.Repositories.Exceptions;
using TCCFatecWorkshop.Repositories;
using TCCFatecWorkshop.Repositories.Interfaces;
using TCCFatecWorkshop.Models.DTO.Product;

namespace TCCFatecWorkshop.Controllers
{
    [Route("User/{userId}/workshop/{workshopId}/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }


        [Authorize(Policy = "UserPolicy")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Product>> Create([FromBody] ProductDTO productDTO)
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



    }
}
