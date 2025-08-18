using Restaurants.Application.Restaurants.Models;

namespace Restaurants.Application.Restaurants.Interfaces
{
	public interface IRestaurantService
	{
		Task<int> CreateAsync(CreateRestaurantRequest request, CancellationToken cancellationToken = default);
		Task<RestaurantDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
		Task<IReadOnlyList<RestaurantDto>> GetAllAsync(CancellationToken cancellationToken = default);
		Task<bool> UpdateAsync(int id, UpdateRestaurantRequest request, CancellationToken cancellationToken = default);
		Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
	}
}


