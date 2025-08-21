using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Auth.Interfaces;
using Restaurants.Application.Auth.Models;

namespace Restaurants.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController(IUserService userService) : ControllerBase
	{
		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<ActionResult<AuthenticatedUser>> Login([FromBody] LoginRequest request, CancellationToken cancellationToken)
		{
			var user = await userService.AuthenticateAsync(request, cancellationToken);
			if (user is null) return Unauthorized();
			return Ok(user);
		}
	}
}


