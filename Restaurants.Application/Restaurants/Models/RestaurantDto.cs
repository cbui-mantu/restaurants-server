namespace Restaurants.Application.Restaurants.Models
{
	public class RestaurantDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string Category { get; set; } = string.Empty;
		public bool HasDelivery { get; set; }
		public string? ContactEmail { get; set; }
		public string? ContactNumber { get; set; }
		public AddressDto? Address { get; set; }
		public List<DishDto> Dishes { get; set; } = new List<DishDto>();
	}

	public class AddressDto
	{
		public string? City { get; set; }
		public string? Street { get; set; }
		public string? PostalCode { get; set; }
	}

	public class DishDto
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public decimal Price { get; set; }
	}
}


