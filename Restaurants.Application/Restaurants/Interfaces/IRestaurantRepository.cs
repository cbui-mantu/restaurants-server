using Restaurants.Domain.Entities;

namespace Restaurants.Application.Restaurants.Interfaces
{
    public interface IRestaurantRepository
    {
        Task<int> CreateAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
        Task<Restaurant?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<Restaurant>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(Restaurant restaurant, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
