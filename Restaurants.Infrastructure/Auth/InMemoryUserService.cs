using Microsoft.Extensions.Options;
using Restaurants.Application.Auth.Interfaces;
using Restaurants.Application.Auth.Models;

namespace Restaurants.Infrastructure.Auth
{
	public class InMemoryUserService(IOptions<JwtSettings> jwtOptions) : IUserService
	{
		private readonly JwtSettings _jwt = jwtOptions.Value;

		private static readonly List<(string Username, string Password, string Role)> Users =
		[
			("admin", "admin123", "Admin"),
			("user", "user123", "User"),
		];

		public Task<AuthenticatedUser?> AuthenticateAsync(LoginRequest request, CancellationToken cancellationToken)
		{
			var match = Users.FirstOrDefault(u => string.Equals(u.Username, request.Username, StringComparison.OrdinalIgnoreCase)
				&& u.Password == request.Password);

			if (string.IsNullOrEmpty(match.Username))
			{
				return Task.FromResult<AuthenticatedUser?>(null);
			}

			var token = JwtTokenGenerator.GenerateToken(match.Username, match.Role, _jwt);
			return Task.FromResult<AuthenticatedUser?>(new AuthenticatedUser
			{
				Username = match.Username,
				Role = match.Role,
				Token = token
			});
		}
	}
}


