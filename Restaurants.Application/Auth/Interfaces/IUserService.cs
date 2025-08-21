using Restaurants.Application.Auth.Models;

namespace Restaurants.Application.Auth.Interfaces
{
	public interface IUserService
	{
		Task<AuthenticatedUser?> AuthenticateAsync(LoginRequest request, CancellationToken cancellationToken);
	}
}


