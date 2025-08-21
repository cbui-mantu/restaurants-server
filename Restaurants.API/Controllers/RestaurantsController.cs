using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Models;
using Restaurants.Application.Restaurants.Services;

namespace Restaurants.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantsController(
        IGetRestaurantService getService,
        ICreateRestaurantService createService,
        IUpdateRestaurantService updateService,
        IDeleteRestaurantService deleteService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll(CancellationToken cancellationToken)
        {
            var items = await getService.GetAllAsync(cancellationToken);
            return Ok(items);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<RestaurantDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var item = await getService.GetByIdAsync(id, cancellationToken);
            if (item is null) return NotFound();
            return Ok(item);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateRestaurantRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var id = await createService.CreateAsync(request, cancellationToken);

                if (id <= 0)
                {
                    return BadRequest(new { message = "Failed to create restaurant." });
                }

                return CreatedAtAction(nameof(GetById), new { id }, new { id });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"An error occurred: {ex.Message}" });
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateRestaurantRequest request, CancellationToken cancellationToken)
        {
            var updated = await updateService.UpdateAsync(id, request, cancellationToken);

            if (!updated)
                return NotFound(new { message = $"Restaurant with id {id} was not found." });

            return Ok(new { message = $"Restaurant with id {id} was successfully updated." });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var deleted = await deleteService.DeleteAsync(id, cancellationToken);
            if (!deleted)
                return NotFound(new { message = $"Restaurant with id {id} was not found." });

            return Ok(new { message = $"Restaurant with id {id} was successfully deleted." });
        }
    }
}


