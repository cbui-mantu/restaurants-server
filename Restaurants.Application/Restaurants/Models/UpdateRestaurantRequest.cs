namespace Restaurants.Application.Restaurants.Models
{
	public class UpdateRestaurantRequest
	{
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Category { get; set; } = string.Empty;
		public bool HasDelivery { get; set; }
		public string? ContactEmail { get; set; }
		public string? ContactNumber { get; set; }
		public AddressDto? Address { get; set; }
	}
}


