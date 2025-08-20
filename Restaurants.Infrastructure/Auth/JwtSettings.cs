namespace Restaurants.Infrastructure.Auth
{
	public class JwtSettings
	{
		public string Issuer { get; set; } = string.Empty;
		public string Audience { get; set; } = string.Empty;
		public string SigningKey { get; set; } = string.Empty;
		public int ExpiryMinutes { get; set; } = 60;
	}
}


