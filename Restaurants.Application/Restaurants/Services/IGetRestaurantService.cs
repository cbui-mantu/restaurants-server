using Restaurants.Application.Restaurants.Models;

namespace Restaurants.Application.Restaurants.Services
{
    public interface IGetRestaurantService
    {
        Task<RestaurantDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<RestaurantDto>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
