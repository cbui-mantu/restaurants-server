using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Restaurants.Infrastructure.Auth
{
	public static class JwtTokenGenerator
	{
		public static string GenerateToken(string username, string role, JwtSettings settings)
		{
			var claims = new List<Claim>
			{
				new(JwtRegisteredClaimNames.Sub, username),
				new(ClaimTypes.Name, username),
				new(ClaimTypes.Role, role)
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SigningKey));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: settings.Issuer,
				audience: settings.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddMinutes(settings.ExpiryMinutes),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}


