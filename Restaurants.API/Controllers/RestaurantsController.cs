using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants.Interfaces;
using Restaurants.Application.Restaurants.Models;

namespace Restaurants.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize(Roles = "Admin")]
	public class RestaurantsController(IRestaurantService restaurantService) : ControllerBase
	{
		[HttpGet]
		public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll(CancellationToken cancellationToken)
		{
			var items = await restaurantService.GetAllAsync(cancellationToken);
			return Ok(items);
		}

		[HttpGet("{id:int}")]
		public async Task<ActionResult<RestaurantDto>> GetById(int id, CancellationToken cancellationToken)
		{
			var item = await restaurantService.GetByIdAsync(id, cancellationToken);
			if (item is null) return NotFound();
			return Ok(item);
		}

		[HttpPost]
		public async Task<ActionResult> Create([FromBody] CreateRestaurantRequest request, CancellationToken cancellationToken)
		{
			var id = await restaurantService.CreateAsync(request, cancellationToken);
			return CreatedAtAction(nameof(GetById), new { id }, null);
		}

		[HttpPut("{id:int}")]
		public async Task<ActionResult> Update(int id, [FromBody] UpdateRestaurantRequest request, CancellationToken cancellationToken)
		{
			var updated = await restaurantService.UpdateAsync(id, request, cancellationToken);
			return updated ? NoContent() : NotFound();
		}

		[HttpDelete("{id:int}")]
		public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
		{
			var deleted = await restaurantService.DeleteAsync(id, cancellationToken);
			return deleted ? NoContent() : NotFound();
		}
	}
}


