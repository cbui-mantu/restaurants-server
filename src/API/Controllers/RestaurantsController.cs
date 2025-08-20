using Application.Restaurants.Create;
using Application.Restaurants.Get;
using Application.Restaurants.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly ISender _sender;

    public RestaurantsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet]
    public async Task<IActionResult> GetRestaurants(CancellationToken cancellationToken)
    {
        var query = new GetRestaurantsQuery();

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRestaurant(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetRestaurantByIdQuery { Id = id };

        var result = await _sender.Send(query, cancellationToken);

        return result.IsSuccess ? Ok(result.Value) : NotFound(result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRestaurant(CreateRestaurantCommand command, CancellationToken cancellationToken)
    {
        var result = await _sender.Send(command, cancellationToken);

        return result.IsSuccess ? CreatedAtAction(nameof(GetRestaurant), new { id = result.Value }, result.Value) : BadRequest(result.Error);
    }
}
